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

public partial class Listas4Page
{
    private IReadOnlyList<ListaWithNavigationPropertiesDto> _listaList { get; set; }
    private MudTable<ListaWithNavigationPropertiesDto> table;
    private GetListasInput Filter { get; set; }

    public Listas4Page()
    {
        // this._logger = logger;

        // NewLista = new ListaCreateDto();
        // EditingLista = new ListaUpdateDto();
        Filter = new GetListasInput
        {
            MaxResultCount = 10,
            SkipCount = 0,
            Sorting = null,
            FilterText = null
        };
    }

    protected override async void OnParametersSet()
    {
        await GetListasAsync();
    }

    private async Task GetListasAsync()
    {
        if( table?.RowsPerPage is not null && table?.CurrentPage is not null)
        {
            Filter.MaxResultCount = table.RowsPerPage;
            Filter.SkipCount = table.RowsPerPage * table.CurrentPage;
            Filter.Sorting = null;            
        } else{
            Filter.MaxResultCount = 10;
            Filter.SkipCount = 0;
            Filter.Sorting = null;            
        }

        var result = await ListasAppService.GetListAsync(Filter);
        _listaList = result.Items;

        await InvokeAsync(StateHasChanged);
    }

    private async void OnSearch(string text)
    {
        Filter.FilterText = text;
        await GetListasAsync();
    }

}