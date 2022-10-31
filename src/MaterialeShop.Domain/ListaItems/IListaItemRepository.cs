using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MaterialeShop.ListaItems
{
    public interface IListaItemRepository : IRepository<ListaItem, Guid>
    {
        Task<ListaItemWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ListaItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string descricao = null,
            string quantidade = null,
            string unidadeMedida = null,
            Guid? listaId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ListaItem>> GetListAsync(
                    string filterText = null,
                    string descricao = null,
                    string quantidade = null,
                    string unidadeMedida = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string descricao = null,
            string quantidade = null,
            string unidadeMedida = null,
            Guid? listaId = null,
            CancellationToken cancellationToken = default);
    }
}