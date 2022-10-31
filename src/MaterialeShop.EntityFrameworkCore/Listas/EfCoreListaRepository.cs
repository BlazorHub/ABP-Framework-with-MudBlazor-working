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

namespace MaterialeShop.Listas
{
    public class EfCoreListaRepository : EfCoreRepository<MaterialeShopDbContext, Lista, Guid>, IListaRepository
    {
        public EfCoreListaRepository(IDbContextProvider<MaterialeShopDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Lista>> GetListAsync(
            string filterText = null,
            string titulo = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, titulo);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ListaConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string titulo = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, titulo);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Lista> ApplyFilter(
            IQueryable<Lista> query,
            string filterText,
            string titulo = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Titulo.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(titulo), e => e.Titulo.Contains(titulo));
        }
    }
}