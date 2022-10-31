using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using MaterialeShop.Shared;

namespace MaterialeShop.Enderecos
{
    public interface IEnderecosAppService : IApplicationService
    {
        Task<PagedResultDto<EnderecoDto>> GetListAsync(GetEnderecosInput input);

        Task<EnderecoDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<EnderecoDto> CreateAsync(EnderecoCreateDto input);

        Task<EnderecoDto> UpdateAsync(Guid id, EnderecoUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(EnderecoExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}