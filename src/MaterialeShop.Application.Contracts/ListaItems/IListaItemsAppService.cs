using MaterialeShop.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using MaterialeShop.Shared;

namespace MaterialeShop.ListaItems
{
    public interface IListaItemsAppService : IApplicationService
    {
        Task<PagedResultDto<ListaItemWithNavigationPropertiesDto>> GetListAsync(GetListaItemsInput input);

        Task<ListaItemWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ListaItemDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetListaLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ListaItemDto> CreateAsync(ListaItemCreateDto input);

        Task<ListaItemDto> UpdateAsync(Guid id, ListaItemUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ListaItemExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}