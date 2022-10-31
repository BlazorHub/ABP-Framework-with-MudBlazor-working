using MaterialeShop.Enderecos;
using MaterialeShop.ListaItems;
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

        CreateMap<ListaItem, ListaItemDto>();
        CreateMap<ListaItem, ListaItemExcelDto>();

        CreateMap<ListaItemWithNavigationProperties, ListaItemWithNavigationPropertiesDto>();
        CreateMap<Lista, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Titulo));

        CreateMap<Endereco, EnderecoDto>();
        CreateMap<Endereco, EnderecoExcelDto>();

        CreateMap<ListaWithNavigationProperties, ListaWithNavigationPropertiesDto>();
        CreateMap<Endereco, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.EnderecoCompleto));
    }
}