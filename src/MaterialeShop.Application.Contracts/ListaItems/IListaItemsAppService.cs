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
        Task<PagedResultDto<ListaItemDto>> GetListAsync(GetListaItemsInput input);

        Task<ListaItemDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ListaItemDto> CreateAsync(ListaItemCreateDto input);

        Task<ListaItemDto> UpdateAsync(Guid id, ListaItemUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ListaItemExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}