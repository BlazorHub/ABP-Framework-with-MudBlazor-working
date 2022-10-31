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
using MaterialeShop.Enderecos;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using MaterialeShop.Shared;

namespace MaterialeShop.Enderecos
{
    [RemoteService(IsEnabled = false)]
    [Authorize(MaterialeShopPermissions.Enderecos.Default)]
    public class EnderecosAppService : ApplicationService, IEnderecosAppService
    {
        private readonly IDistributedCache<EnderecoExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly EnderecoManager _enderecoManager;

        public EnderecosAppService(IEnderecoRepository enderecoRepository, EnderecoManager enderecoManager, IDistributedCache<EnderecoExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _enderecoRepository = enderecoRepository;
            _enderecoManager = enderecoManager;
        }

        public virtual async Task<PagedResultDto<EnderecoDto>> GetListAsync(GetEnderecosInput input)
        {
            var totalCount = await _enderecoRepository.GetCountAsync(input.FilterText, input.EnderecoCompleto);
            var items = await _enderecoRepository.GetListAsync(input.FilterText, input.EnderecoCompleto, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EnderecoDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Endereco>, List<EnderecoDto>>(items)
            };
        }

        public virtual async Task<EnderecoDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Endereco, EnderecoDto>(await _enderecoRepository.GetAsync(id));
        }

        [Authorize(MaterialeShopPermissions.Enderecos.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _enderecoRepository.DeleteAsync(id);
        }

        [Authorize(MaterialeShopPermissions.Enderecos.Create)]
        public virtual async Task<EnderecoDto> CreateAsync(EnderecoCreateDto input)
        {

            var endereco = await _enderecoManager.CreateAsync(
            input.EnderecoCompleto
            );

            return ObjectMapper.Map<Endereco, EnderecoDto>(endereco);
        }

        [Authorize(MaterialeShopPermissions.Enderecos.Edit)]
        public virtual async Task<EnderecoDto> UpdateAsync(Guid id, EnderecoUpdateDto input)
        {

            var endereco = await _enderecoManager.UpdateAsync(
            id,
            input.EnderecoCompleto, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Endereco, EnderecoDto>(endereco);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EnderecoExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _enderecoRepository.GetListAsync(input.FilterText, input.EnderecoCompleto);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Endereco>, List<EnderecoExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Enderecos.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new EnderecoExcelDownloadTokenCacheItem { Token = token },
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