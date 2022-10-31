using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using MaterialeShop.Permissions;
using MaterialeShop.Listas;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using MaterialeShop.Shared;

namespace MaterialeShop.Listas
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MaterialeShopPermissions.Listas.Default)]
    public class ListasAppService : ApplicationService, IListasAppService
    {
        private readonly IDistributedCache<ListaExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IListaRepository _listaRepository;
        private readonly ListaManager _listaManager;

        public ListasAppService(IListaRepository listaRepository, ListaManager listaManager, IDistributedCache<ListaExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _listaRepository = listaRepository;
            _listaManager = listaManager;
        }

        public virtual async Task<PagedResultDto<ListaDto>> GetListAsync(GetListasInput input)
        {
            var totalCount = await _listaRepository.GetCountAsync(input.FilterText, input.Titulo);
            var items = await _listaRepository.GetListAsync(input.FilterText, input.Titulo, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ListaDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Lista>, List<ListaDto>>(items)
            };
        }

        public virtual async Task<ListaDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Lista, ListaDto>(await _listaRepository.GetAsync(id));
        }

        [Authorize(MaterialeShopPermissions.Listas.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _listaRepository.DeleteAsync(id);
        }

        [Authorize(MaterialeShopPermissions.Listas.Create)]
        public virtual async Task<ListaDto> CreateAsync(ListaCreateDto input)
        {

            var lista = await _listaManager.CreateAsync(
            input.Titulo
            );

            return ObjectMapper.Map<Lista, ListaDto>(lista);
        }

        [Authorize(MaterialeShopPermissions.Listas.Edit)]
        public virtual async Task<ListaDto> UpdateAsync(Guid id, ListaUpdateDto input)
        {

            var lista = await _listaManager.UpdateAsync(
            id,
            input.Titulo, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Lista, ListaDto>(lista);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ListaExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _listaRepository.GetListAsync(input.FilterText, input.Titulo);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Lista>, List<ListaExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Listas.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ListaExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}