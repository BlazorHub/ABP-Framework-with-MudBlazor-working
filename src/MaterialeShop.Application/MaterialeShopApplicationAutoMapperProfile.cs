using System;
using MaterialeShop.Shared;
using Volo.Abp.AutoMapper;
using MaterialeShop.Listas;
using AutoMapper;

namespace MaterialeShop;

public class MaterialeShopApplicationAutoMapperProfile : Profile
{
    public MaterialeShopApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Lista, ListaDto>();
        CreateMap<Lista, ListaExcelDto>();
    }
}