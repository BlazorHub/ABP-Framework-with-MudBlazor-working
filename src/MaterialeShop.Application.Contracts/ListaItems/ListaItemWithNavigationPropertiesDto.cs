using MaterialeShop.Listas;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace MaterialeShop.ListaItems
{
    public class ListaItemWithNavigationPropertiesDto
    {
        public ListaItemDto ListaItem { get; set; }

        public ListaDto Lista { get; set; }

    }
}