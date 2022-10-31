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

        public async Task<ListaWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(lista => new ListaWithNavigationProperties
                {
                    Lista = lista,
                    Endereco = dbContext.Enderecos.FirstOrDefault(c => c.Id == lista.EnderecoId)
                }).FirstOrDefault();
        }

        public async Task<List<ListaWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string titulo = null,
            Guid? enderecoId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, titulo, enderecoId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ListaConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ListaWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from lista in (await GetDbSetAsync())
                   join endereco in (await GetDbContextAsync()).Enderecos on lista.EnderecoId equals endereco.Id into enderecos
                   from endereco in enderecos.DefaultIfEmpty()

                   select new ListaWithNavigationProperties
                   {
                       Lista = lista,
                       Endereco = endereco
                   };
        }

        protected virtual IQueryable<ListaWithNavigationProperties> ApplyFilter(
            IQueryable<ListaWithNavigationProperties> query,
            string filterText,
            string titulo = null,
            Guid? enderecoId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Lista.Titulo.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(titulo), e => e.Lista.Titulo.Contains(titulo))
                    .WhereIf(enderecoId != null && enderecoId != Guid.Empty, e => e.Endereco != null && e.Endereco.Id == enderecoId);
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
            Guid? enderecoId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, titulo, enderecoId);
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