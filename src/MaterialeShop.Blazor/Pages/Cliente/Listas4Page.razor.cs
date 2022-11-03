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
    private GetListasInput Filter { get; set; }

    protected override async void OnParametersSet()
    {
        await GetListasAsync();
    }

    private async Task GetListasAsync()
    {
        Filter = new GetListasInput
        {
            MaxResultCount = 50,
            SkipCount = 0,
            Sorting = null
        };

        // Filter.MaxResultCount = 50;
        // Filter.SkipCount = 0;
        // Filter.Sorting = null;

        var result = await ListasAppService.GetListAsync(Filter);
        _listaList = result.Items;
        // TotalCount = (int)result.TotalCount;
    }

}