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

public partial class Listas3Page
{
    private readonly ILogger<Listas3Page> _logger;
    private GetListasInput Filter { get; set; }
    // private IEnumerable<ListaWithNavigationPropertiesDto> pagedData;
    private MudTable<ListaWithNavigationPropertiesDto> table;

    // private int totalItems;
    private string searchString = null;

    // public Listas3Page(ILogger<Listas3Page> logger)
    public Listas3Page()
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

    protected override async Task OnParametersSetAsync()
    {
        // await GetListasAsync();
    }

    string sort_direction = "";
    string sort_label = "";
    private async Task<TableData<ListaWithNavigationPropertiesDto>> ServerReload(TableState state)
    {

        // Filter.MaxResultCount = 10;//state.PageSize;
        // Filter.SkipCount = (state.Page - 1) * state.PageSize;
        // Filter.Sorting = null;
        
        sort_direction = state.SortDirection.ToString();
        sort_label = state.SortLabel;
        
        if(state.SortLabel is not null)
        {
            Filter.Sorting = state.SortLabel + (state.SortDirection == MudBlazor.SortDirection.Descending ? " DESC" : " ASC");
        } else
        {
            Filter.Sorting = null;
        }

        // Filter.Sorting = "Lista.Titulo DESC";

        Filter.MaxResultCount = state.PageSize;
        Filter.SkipCount = state.Page * state.PageSize;

        var result = await ListasAppService.GetListAsync(Filter);

        var totalItems = (int)result.TotalCount;
        var pagedData = result.Items;

        // IEnumerable<ListaWithNavigationPropertiesDto> data = await httpClient.GetFromJsonAsync<List<ListaWithNavigationPropertiesDto>>("webapi/periodictable");
        
        // await Task.Delay(300);
        
        // data = data.Where(element =>
        // {
        //     if (string.IsNullOrWhiteSpace(searchString))
        //         return true;
        //     if (element.Sign.Contains(searchString, StringComparison.OrdinalIgnoreCase))
        //         return true;
        //     if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
        //         return true;
        //     if ($"{element.Number} {element.Position} {element.Molar}".Contains(searchString))
        //         return true;
        //     return false;
        // }).ToArray();

        

        // switch (state.SortLabel)
        // {
        //     case "nr_field":
        //         data = data.OrderByDirection(state.SortDirection, o => o.Number);
        //         break;
        //     case "sign_field":
        //         data = data.OrderByDirection(state.SortDirection, o => o.Sign);
        //         break;
        //     case "name_field":
        //         data = data.OrderByDirection(state.SortDirection, o => o.Name);
        //         break;
        //     case "position_field":
        //         data = data.OrderByDirection(state.SortDirection, o => o.Position);
        //         break;
        //     case "mass_field":
        //         data = data.OrderByDirection(state.SortDirection, o => o.Molar);
        //         break;
        // }

        // pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
        
        await InvokeAsync(StateHasChanged);

        return new TableData<ListaWithNavigationPropertiesDto>() { TotalItems = totalItems, Items = pagedData };
    }

    private void OnSearch(string text)
    {
        Filter.FilterText = text;
        table.ReloadServerData();
    }


    // private async Task GetListasAsync()
    // {
    //     _loadingLista = true;
    //     // await Task.Delay(5000);

    //     // CurrentSorting = e.Columns
    //     //     .Where(c => c.SortDirection != SortDirection.Default)
    //     //     .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
    //     //     .JoinAsString(",");
    //     // CurrentPage = e.Page; 

    //     Filter.MaxResultCount = PageSize;
    //     Filter.SkipCount = (CurrentPage - 1) * PageSize;
    //     Filter.Sorting = CurrentSorting;

    //     var result = await ListasAppService.GetListAsync(Filter);
    //     ListaList = result.Items;
    //     TotalCount = (int)result.TotalCount;
    //     _loadingLista = false;
    // }


}