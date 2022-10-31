using Volo.Abp.AutoMapper;
using MaterialeShop.Listas;
using AutoMapper;

namespace MaterialeShop.Blazor;

public class MaterialeShopBlazorAutoMapperProfile : Profile
{
    public MaterialeShopBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.

        CreateMap<ListaDto, ListaUpdateDto>();
    }
}