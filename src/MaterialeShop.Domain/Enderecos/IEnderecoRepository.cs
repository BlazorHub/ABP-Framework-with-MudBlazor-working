using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MaterialeShop.Enderecos
{
    public interface IEnderecoRepository : IRepository<Endereco, Guid>
    {
        Task<List<Endereco>> GetListAsync(
            string filterText = null,
            string enderecoCompleto = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string enderecoCompleto = null,
            CancellationToken cancellationToken = default);
    }
}