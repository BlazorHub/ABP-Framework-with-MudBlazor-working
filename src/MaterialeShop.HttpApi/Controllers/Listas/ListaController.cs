using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using MaterialeShop.Listas;
using Volo.Abp.Content;
using MaterialeShop.Shared;

namespace MaterialeShop.Controllers.Listas
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Lista")]
    [Route("api/app/listas")]

    public class ListaController : AbpController, IListasAppService
    {
        private readonly IListasAppService _listasAppService;

        public ListaController(IListasAppService listasAppService)
        {
            _listasAppService = listasAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ListaDto>> GetListAsync(GetListasInput input)
        {
            return _listasAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ListaDto> GetAsync(Guid id)
        {
            return _listasAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ListaDto> CreateAsync(ListaCreateDto input)
        {
            return _listasAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ListaDto> UpdateAsync(Guid id, ListaUpdateDto input)
        {
            return _listasAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _listasAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ListaExcelDownloadDto input)
        {
            return _listasAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _listasAppService.GetDownloadTokenAsync();
        }
    }
}