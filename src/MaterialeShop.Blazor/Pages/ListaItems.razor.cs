using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using MaterialeShop.ListaItems;
using MaterialeShop.Permissions;
using MaterialeShop.Shared;

namespace MaterialeShop.Blazor.Pages
{
    public partial class ListaItems
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<ListaItemDto> ListaItemList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateListaItem { get; set; }
        private bool CanEditListaItem { get; set; }
        private bool CanDeleteListaItem { get; set; }
        private ListaItemCreateDto NewListaItem { get; set; }
        private Validations NewListaItemValidations { get; set; }
        private ListaItemUpdateDto EditingListaItem { get; set; }
        private Validations EditingListaItemValidations { get; set; }
        private Guid EditingListaItemId { get; set; }
        private Modal CreateListaItemModal { get; set; }
        private Modal EditListaItemModal { get; set; }
        private GetListaItemsInput Filter { get; set; }
        private DataGridEntityActionsColumn<ListaItemDto> EntityActionsColumn { get; set; }
        protected string SelectedCreateTab = "listaItem-create-tab";
        protected string SelectedEditTab = "listaItem-edit-tab";
        
        public ListaItems()
        {
            NewListaItem = new ListaItemCreateDto();
            EditingListaItem = new ListaItemUpdateDto();
            Filter = new GetListaItemsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:ListaItems"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewListaItem"], async () =>
            {
                await OpenCreateListaItemModalAsync();
            }, IconName.Add, requiredPolicyName: MaterialeShopPermissions.ListaItems.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateListaItem = await AuthorizationService
                .IsGrantedAsync(MaterialeShopPermissions.ListaItems.Create);
            CanEditListaItem = await AuthorizationService
                            .IsGrantedAsync(MaterialeShopPermissions.ListaItems.Edit);
            CanDeleteListaItem = await AuthorizationService
                            .IsGrantedAsync(MaterialeShopPermissions.ListaItems.Delete);
        }

        private async Task GetListaItemsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await ListaItemsAppService.GetListAsync(Filter);
            ListaItemList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetListaItemsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await ListaItemsAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Default");
            NavigationManager.NavigateTo($"{remoteService.BaseUrl.EnsureEndsWith('/')}api/app/lista-items/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ListaItemDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetListaItemsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateListaItemModalAsync()
        {
            NewListaItem = new ListaItemCreateDto{
                
                
            };
            await NewListaItemValidations.ClearAll();
            await CreateListaItemModal.Show();
        }

        private async Task CloseCreateListaItemModalAsync()
        {
            NewListaItem = new ListaItemCreateDto{
                
                
            };
            await CreateListaItemModal.Hide();
        }

        private async Task OpenEditListaItemModalAsync(ListaItemDto input)
        {
            var listaItem = await ListaItemsAppService.GetAsync(input.Id);
            
            EditingListaItemId = listaItem.Id;
            EditingListaItem = ObjectMapper.Map<ListaItemDto, ListaItemUpdateDto>(listaItem);
            await EditingListaItemValidations.ClearAll();
            await EditListaItemModal.Show();
        }

        private async Task DeleteListaItemAsync(ListaItemDto input)
        {
            await ListaItemsAppService.DeleteAsync(input.Id);
            await GetListaItemsAsync();
        }

        private async Task CreateListaItemAsync()
        {
            try
            {
                if (await NewListaItemValidations.ValidateAll() == false)
                {
                    return;
                }

                await ListaItemsAppService.CreateAsync(NewListaItem);
                await GetListaItemsAsync();
                await CloseCreateListaItemModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditListaItemModalAsync()
        {
            await EditListaItemModal.Hide();
        }

        private async Task UpdateListaItemAsync()
        {
            try
            {
                if (await EditingListaItemValidations.ValidateAll() == false)
                {
                    return;
                }

                await ListaItemsAppService.UpdateAsync(EditingListaItemId, EditingListaItem);
                await GetListaItemsAsync();
                await EditListaItemModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }
        

    }
}
