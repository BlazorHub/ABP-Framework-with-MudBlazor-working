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
using MaterialeShop.Enderecos;
using MaterialeShop.Permissions;
using MaterialeShop.Shared;

namespace MaterialeShop.Blazor.Pages
{
    public partial class Enderecos
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<EnderecoDto> EnderecoList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateEndereco { get; set; }
        private bool CanEditEndereco { get; set; }
        private bool CanDeleteEndereco { get; set; }
        private EnderecoCreateDto NewEndereco { get; set; }
        private Validations NewEnderecoValidations { get; set; }
        private EnderecoUpdateDto EditingEndereco { get; set; }
        private Validations EditingEnderecoValidations { get; set; }
        private Guid EditingEnderecoId { get; set; }
        private Modal CreateEnderecoModal { get; set; }
        private Modal EditEnderecoModal { get; set; }
        private GetEnderecosInput Filter { get; set; }
        private DataGridEntityActionsColumn<EnderecoDto> EntityActionsColumn { get; set; }
        protected string SelectedCreateTab = "endereco-create-tab";
        protected string SelectedEditTab = "endereco-edit-tab";
        
        public Enderecos()
        {
            NewEndereco = new EnderecoCreateDto();
            EditingEndereco = new EnderecoUpdateDto();
            Filter = new GetEnderecosInput
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
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Enderecos"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewEndereco"], async () =>
            {
                await OpenCreateEnderecoModalAsync();
            }, IconName.Add, requiredPolicyName: MaterialeShopPermissions.Enderecos.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateEndereco = await AuthorizationService
                .IsGrantedAsync(MaterialeShopPermissions.Enderecos.Create);
            CanEditEndereco = await AuthorizationService
                            .IsGrantedAsync(MaterialeShopPermissions.Enderecos.Edit);
            CanDeleteEndereco = await AuthorizationService
                            .IsGrantedAsync(MaterialeShopPermissions.Enderecos.Delete);
        }

        private async Task GetEnderecosAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await EnderecosAppService.GetListAsync(Filter);
            EnderecoList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetEnderecosAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await EnderecosAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Default");
            NavigationManager.NavigateTo($"{remoteService.BaseUrl.EnsureEndsWith('/')}api/app/enderecos/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<EnderecoDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetEnderecosAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateEnderecoModalAsync()
        {
            NewEndereco = new EnderecoCreateDto{
                
                
            };
            await NewEnderecoValidations.ClearAll();
            await CreateEnderecoModal.Show();
        }

        private async Task CloseCreateEnderecoModalAsync()
        {
            NewEndereco = new EnderecoCreateDto{
                
                
            };
            await CreateEnderecoModal.Hide();
        }

        private async Task OpenEditEnderecoModalAsync(EnderecoDto input)
        {
            var endereco = await EnderecosAppService.GetAsync(input.Id);
            
            EditingEnderecoId = endereco.Id;
            EditingEndereco = ObjectMapper.Map<EnderecoDto, EnderecoUpdateDto>(endereco);
            await EditingEnderecoValidations.ClearAll();
            await EditEnderecoModal.Show();
        }

        private async Task DeleteEnderecoAsync(EnderecoDto input)
        {
            await EnderecosAppService.DeleteAsync(input.Id);
            await GetEnderecosAsync();
        }

        private async Task CreateEnderecoAsync()
        {
            try
            {
                if (await NewEnderecoValidations.ValidateAll() == false)
                {
                    return;
                }

                await EnderecosAppService.CreateAsync(NewEndereco);
                await GetEnderecosAsync();
                await CloseCreateEnderecoModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditEnderecoModalAsync()
        {
            await EditEnderecoModal.Hide();
        }

        private async Task UpdateEnderecoAsync()
        {
            try
            {
                if (await EditingEnderecoValidations.ValidateAll() == false)
                {
                    return;
                }

                await EnderecosAppService.UpdateAsync(EditingEnderecoId, EditingEndereco);
                await GetEnderecosAsync();
                await EditEnderecoModal.Hide();                
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
