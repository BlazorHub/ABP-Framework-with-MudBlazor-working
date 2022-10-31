using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MaterialeShop.Listas
{
    public interface IListaRepository : IRepository<Lista, Guid>
    {
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
            CancellationToken cancellationToken = default);
    }
}