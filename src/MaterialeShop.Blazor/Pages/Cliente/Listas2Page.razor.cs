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
using MaterialeShop.Listas;
using MaterialeShop.Permissions;
using MaterialeShop.Shared;
using MudBlazor;
using Microsoft.Extensions.Logging;

namespace MaterialeShop.Blazor.Pages.Cliente;

public partial class Listas2Page
{
    protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
    protected PageToolbar Toolbar { get; } = new PageToolbar();
    private IReadOnlyList<ListaWithNavigationPropertiesDto> ListaList { get; set; }
    // private MudDataGrid<ListaWithNavigationPropertiesDto> _listaList;
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; } = 1;
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }
    private bool CanCreateLista { get; set; }
    private bool CanEditLista { get; set; }
    private bool CanDeleteLista { get; set; }
    private ListaCreateDto NewLista { get; set; }
    private Validations NewListaValidations { get; set; }
    private ListaUpdateDto EditingLista { get; set; }
    private Validations EditingListaValidations { get; set; }
    private Guid EditingListaId { get; set; }
    private Modal CreateListaModal { get; set; }
    private Modal EditListaModal { get; set; }
    private GetListasInput Filter { get; set; }
    private DataGridEntityActionsColumn<ListaWithNavigationPropertiesDto> EntityActionsColumn { get; set; }
    protected string SelectedCreateTab = "lista-create-tab";
    protected string SelectedEditTab = "lista-edit-tab";
    private IReadOnlyList<LookupDto<Guid?>> EnderecosNullable { get; set; } = new List<LookupDto<Guid?>>();
    private readonly ILogger<ListasPage> _logger;
    private bool _loadingLista = true;

    public Listas2Page(ILogger<ListasPage> logger)
    {
        NewLista = new ListaCreateDto();
        EditingLista = new ListaUpdateDto();
        Filter = new GetListasInput
        {
            MaxResultCount = PageSize,
            SkipCount = (CurrentPage - 1) * PageSize,
            Sorting = CurrentSorting
        };
        this._logger = logger;
    }

    protected override async Task OnParametersSetAsync()
    {
        await GetListasAsync();
    }

    private async Task GetListasAsync()
    {
        _loadingLista = true;
        // await Task.Delay(5000);

        // CurrentSorting = e.Columns
        //     .Where(c => c.SortDirection != SortDirection.Default)
        //     .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
        //     .JoinAsString(",");
        // CurrentPage = e.Page; 

        Filter.MaxResultCount = PageSize;
        Filter.SkipCount = (CurrentPage - 1) * PageSize;
        Filter.Sorting = CurrentSorting;

        var result = await ListasAppService.GetListAsync(Filter);
        ListaList = result.Items;
        TotalCount = (int)result.TotalCount;
        _loadingLista = false;
    }


}