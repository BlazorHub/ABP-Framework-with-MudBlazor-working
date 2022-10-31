using MaterialeShop.Shared;
using MaterialeShop.Enderecos;
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
        private readonly IRepository<Endereco, Guid> _enderecoRepository;

        public ListasAppService(IListaRepository listaRepository, ListaManager listaManager, IDistributedCache<ListaExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Endereco, Guid> enderecoRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _listaRepository = listaRepository;
            _listaManager = listaManager; _enderecoRepository = enderecoRepository;
        }

        public virtual async Task<PagedResultDto<ListaWithNavigationPropertiesDto>> GetListAsync(GetListasInput input)
        {
            var totalCount = await _listaRepository.GetCountAsync(input.FilterText, input.Titulo, input.EnderecoId);
            var items = await _listaRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Titulo, input.EnderecoId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ListaWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ListaWithNavigationProperties>, List<ListaWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ListaWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ListaWithNavigationProperties, ListaWithNavigationPropertiesDto>
                (await _listaRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ListaDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Lista, ListaDto>(await _listaRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetEnderecoLookupAsync(LookupRequestDto input)
        {
            var query = (await _enderecoRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.EnderecoCompleto != null &&
                         x.EnderecoCompleto.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Endereco>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Endereco>, List<LookupDto<Guid?>>>(lookupData)
            };
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
            input.EnderecoId, input.Titulo
            );

            return ObjectMapper.Map<Lista, ListaDto>(lista);
        }

        [Authorize(MaterialeShopPermissions.Listas.Edit)]
        public virtual async Task<ListaDto> UpdateAsync(Guid id, ListaUpdateDto input)
        {

            var lista = await _listaManager.UpdateAsync(
            id,
            input.EnderecoId, input.Titulo, input.ConcurrencyStamp
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