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

        public async Task<ListaItemWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(listaItem => new ListaItemWithNavigationProperties
                {
                    ListaItem = listaItem,
                    Lista = dbContext.Listas.FirstOrDefault(c => c.Id == listaItem.ListaId)
                }).FirstOrDefault();
        }

        public async Task<List<ListaItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string descricao = null,
            string quantidade = null,
            string unidadeMedida = null,
            Guid? listaId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, descricao, quantidade, unidadeMedida, listaId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ListaItemConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ListaItemWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from listaItem in (await GetDbSetAsync())
                   join lista in (await GetDbContextAsync()).Listas on listaItem.ListaId equals lista.Id into listas
                   from lista in listas.DefaultIfEmpty()

                   select new ListaItemWithNavigationProperties
                   {
                       ListaItem = listaItem,
                       Lista = lista
                   };
        }

        protected virtual IQueryable<ListaItemWithNavigationProperties> ApplyFilter(
            IQueryable<ListaItemWithNavigationProperties> query,
            string filterText,
            string descricao = null,
            string quantidade = null,
            string unidadeMedida = null,
            Guid? listaId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ListaItem.Descricao.Contains(filterText) || e.ListaItem.Quantidade.Contains(filterText) || e.ListaItem.UnidadeMedida.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(descricao), e => e.ListaItem.Descricao.Contains(descricao))
                    .WhereIf(!string.IsNullOrWhiteSpace(quantidade), e => e.ListaItem.Quantidade.Contains(quantidade))
                    .WhereIf(!string.IsNullOrWhiteSpace(unidadeMedida), e => e.ListaItem.UnidadeMedida.Contains(unidadeMedida))
                    .WhereIf(listaId != null && listaId != Guid.Empty, e => e.Lista != null && e.Lista.Id == listaId);
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
            Guid? listaId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, descricao, quantidade, unidadeMedida, listaId);
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