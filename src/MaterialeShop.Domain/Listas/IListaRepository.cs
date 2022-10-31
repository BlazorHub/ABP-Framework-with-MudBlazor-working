using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MaterialeShop.Listas
{
    public interface IListaRepository : IRepository<Lista, Guid>
    {
        Task<ListaWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ListaWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string titulo = null,
            Guid? enderecoId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Lista>> GetListAsync(
                    string filterText = null,
                    string titulo = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string titulo = null,
            Guid? enderecoId = null,
            CancellationToken cancellationToken = default);
    }
}