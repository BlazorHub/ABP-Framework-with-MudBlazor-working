using AutoMapper;
using MaterialeShop.Enderecos;
using MaterialeShop.ListaItems;
using MaterialeShop.Listas;
using Volo.Abp.AutoMapper;

namespace MaterialeShop.Admin
{
    public class MaterialeShopBlazorAutoMapperProfile : Profile
    {
        public MaterialeShopBlazorAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Blazor project.

            CreateMap<ListaDto, ListaUpdateDto>();

            CreateMap<ListaItemDto, ListaItemUpdateDto>();

            CreateMap<EnderecoDto, EnderecoUpdateDto>();
        }
    }
}