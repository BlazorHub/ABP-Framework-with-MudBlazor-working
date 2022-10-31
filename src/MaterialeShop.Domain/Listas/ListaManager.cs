using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace MaterialeShop.Listas
{
    public class ListaManager : DomainService
    {
        private readonly IListaRepository _listaRepository;

        public ListaManager(IListaRepository listaRepository)
        {
            _listaRepository = listaRepository;
        }

        public async Task<Lista> CreateAsync(
        string titulo)
        {
            var lista = new Lista(
             GuidGenerator.Create(),
             titulo
             );

            return await _listaRepository.InsertAsync(lista);
        }

        public async Task<Lista> UpdateAsync(
            Guid id,
            string titulo, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _listaRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var lista = await AsyncExecuter.FirstOrDefaultAsync(query);

            lista.Titulo = titulo;

            lista.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _listaRepository.UpdateAsync(lista);
        }

    }
}