using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace MaterialeShop.Enderecos
{
    public class EnderecoManager : DomainService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoManager(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Endereco> CreateAsync(
        string enderecoCompleto)
        {
            var endereco = new Endereco(
             GuidGenerator.Create(),
             enderecoCompleto
             );

            return await _enderecoRepository.InsertAsync(endereco);
        }

        public async Task<Endereco> UpdateAsync(
            Guid id,
            string enderecoCompleto, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _enderecoRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var endereco = await AsyncExecuter.FirstOrDefaultAsync(query);

            endereco.EnderecoCompleto = enderecoCompleto;

            endereco.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _enderecoRepository.UpdateAsync(endereco);
        }

    }
}