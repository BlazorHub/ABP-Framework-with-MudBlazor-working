using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using MaterialeShop.EntityFrameworkCore;

namespace MaterialeShop.Enderecos
{
    public class EfCoreEnderecoRepository : EfCoreRepository<MaterialeShopDbContext, Endereco, Guid>, IEnderecoRepository
    {
        public EfCoreEnderecoRepository(IDbContextProvider<MaterialeShopDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Endereco>> GetListAsync(
            string filterText = null,
            string enderecoCompleto = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, enderecoCompleto);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EnderecoConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string enderecoCompleto = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, enderecoCompleto);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Endereco> ApplyFilter(
            IQueryable<Endereco> query,
            string filterText,
            string enderecoCompleto = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EnderecoCompleto.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(enderecoCompleto), e => e.EnderecoCompleto.Contains(enderecoCompleto));
        }
    }
}