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
using MaterialeShop.ListaItems;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using MaterialeShop.Shared;

namespace MaterialeShop.ListaItems
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MaterialeShopPermissions.ListaItems.Default)]
    public class ListaItemsAppService : ApplicationService, IListaItemsAppService
    {
        private readonly IDistributedCache<ListaItemExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IListaItemRepository _listaItemRepository;
        private readonly ListaItemManager _listaItemManager;

        public ListaItemsAppService(IListaItemRepository listaItemRepository, ListaItemManager listaItemManager, IDistributedCache<ListaItemExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _listaItemRepository = listaItemRepository;
            _listaItemManager = listaItemManager;
        }

        public virtual async Task<PagedResultDto<ListaItemDto>> GetListAsync(GetListaItemsInput input)
        {
            var totalCount = await _listaItemRepository.GetCountAsync(input.FilterText, input.Descricao, input.Quantidade, input.UnidadeMedida);
            var items = await _listaItemRepository.GetListAsync(input.FilterText, input.Descricao, input.Quantidade, input.UnidadeMedida, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ListaItemDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ListaItem>, List<ListaItemDto>>(items)
            };
        }

        public virtual async Task<ListaItemDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ListaItem, ListaItemDto>(await _listaItemRepository.GetAsync(id));
        }

        [Authorize(MaterialeShopPermissions.ListaItems.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _listaItemRepository.DeleteAsync(id);
        }

        [Authorize(MaterialeShopPermissions.ListaItems.Create)]
        public virtual async Task<ListaItemDto> CreateAsync(ListaItemCreateDto input)
        {

            var listaItem = await _listaItemManager.CreateAsync(
            input.Descricao, input.Quantidade, input.UnidadeMedida
            );

            return ObjectMapper.Map<ListaItem, ListaItemDto>(listaItem);
        }

        [Authorize(MaterialeShopPermissions.ListaItems.Edit)]
        public virtual async Task<ListaItemDto> UpdateAsync(Guid id, ListaItemUpdateDto input)
        {

            var listaItem = await _listaItemManager.UpdateAsync(
            id,
            input.Descricao, input.Quantidade, input.UnidadeMedida, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ListaItem, ListaItemDto>(listaItem);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ListaItemExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _listaItemRepository.GetListAsync(input.FilterText, input.Descricao, input.Quantidade, input.UnidadeMedida);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ListaItem>, List<ListaItemExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ListaItems.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ListaItemExcelDownloadTokenCacheItem { Token = token },
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