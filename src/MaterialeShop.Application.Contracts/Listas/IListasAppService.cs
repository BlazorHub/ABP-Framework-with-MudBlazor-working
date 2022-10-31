using MaterialeShop.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using MaterialeShop.Shared;

namespace MaterialeShop.Listas
{
    public interface IListasAppService : IApplicationService
    {
        Task<PagedResultDto<ListaWithNavigationPropertiesDto>> GetListAsync(GetListasInput input);

        Task<ListaWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ListaDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetEnderecoLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ListaDto> CreateAsync(ListaCreateDto input);

        Task<ListaDto> UpdateAsync(Guid id, ListaUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ListaExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}