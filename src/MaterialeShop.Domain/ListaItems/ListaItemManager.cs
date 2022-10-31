using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace MaterialeShop.ListaItems
{
    public class ListaItemManager : DomainService
    {
        private readonly IListaItemRepository _listaItemRepository;

        public ListaItemManager(IListaItemRepository listaItemRepository)
        {
            _listaItemRepository = listaItemRepository;
        }

        public async Task<ListaItem> CreateAsync(
        string descricao, string quantidade, string unidadeMedida)
        {
            var listaItem = new ListaItem(
             GuidGenerator.Create(),
             descricao, quantidade, unidadeMedida
             );

            return await _listaItemRepository.InsertAsync(listaItem);
        }

        public async Task<ListaItem> UpdateAsync(
            Guid id,
            string descricao, string quantidade, string unidadeMedida, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _listaItemRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var listaItem = await AsyncExecuter.FirstOrDefaultAsync(query);

            listaItem.Descricao = descricao;
            listaItem.Quantidade = quantidade;
            listaItem.UnidadeMedida = unidadeMedida;

            listaItem.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _listaItemRepository.UpdateAsync(listaItem);
        }

    }
}