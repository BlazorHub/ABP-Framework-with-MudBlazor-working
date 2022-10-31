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

namespace MaterialeShop.ListaItems
{
    public class EfCoreListaItemRepository : EfCoreRepository<MaterialeShopDbContext, ListaItem, Guid>, IListaItemRepository
    {
        public EfCoreListaItemRepository(IDbContextProvider<MaterialeShopDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<ListaItem>> GetListAsync(
            string filterText = null,
            string descricao = null,
            string quantidade = null,
            string unidadeMedida = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, descricao, quantidade, unidadeMedida);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ListaItemConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string descricao = null,
            string quantidade = null,
            string unidadeMedida = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, descricao, quantidade, unidadeMedida);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ListaItem> ApplyFilter(
            IQueryable<ListaItem> query,
            string filterText,
            string descricao = null,
            string quantidade = null,
            string unidadeMedida = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Descricao.Contains(filterText) || e.Quantidade.Contains(filterText) || e.UnidadeMedida.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(descricao), e => e.Descricao.Contains(descricao))
                    .WhereIf(!string.IsNullOrWhiteSpace(quantidade), e => e.Quantidade.Contains(quantidade))
                    .WhereIf(!string.IsNullOrWhiteSpace(unidadeMedida), e => e.UnidadeMedida.Contains(unidadeMedida));
        }
    }
}