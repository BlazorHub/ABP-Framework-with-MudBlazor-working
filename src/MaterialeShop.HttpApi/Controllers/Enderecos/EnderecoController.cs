using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using MaterialeShop.Enderecos;
using Volo.Abp.Content;
using MaterialeShop.Shared;

namespace MaterialeShop.Controllers.Enderecos
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Endereco")]
    [Route("api/app/enderecos")]

    public class EnderecoController : AbpController, IEnderecosAppService
    {
        private readonly IEnderecosAppService _enderecosAppService;

        public EnderecoController(IEnderecosAppService enderecosAppService)
        {
            _enderecosAppService = enderecosAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<EnderecoDto>> GetListAsync(GetEnderecosInput input)
        {
            return _enderecosAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<EnderecoDto> GetAsync(Guid id)
        {
            return _enderecosAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<EnderecoDto> CreateAsync(EnderecoCreateDto input)
        {
            return _enderecosAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<EnderecoDto> UpdateAsync(Guid id, EnderecoUpdateDto input)
        {
            return _enderecosAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _enderecosAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(EnderecoExcelDownloadDto input)
        {
            return _enderecosAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _enderecosAppService.GetDownloadTokenAsync();
        }
    }
}