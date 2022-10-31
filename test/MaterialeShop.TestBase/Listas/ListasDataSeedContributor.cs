using MaterialeShop.Enderecos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using MaterialeShop.Listas;

namespace MaterialeShop.Listas
{
    public class ListasDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IListaRepository _listaRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly EnderecosDataSeedContributor _enderecosDataSeedContributor;

        public ListasDataSeedContributor(IListaRepository listaRepository, IUnitOfWorkManager unitOfWorkManager, EnderecosDataSeedContributor enderecosDataSeedContributor)
        {
            _listaRepository = listaRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _enderecosDataSeedContributor = enderecosDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _enderecosDataSeedContributor.SeedAsync(context);

            await _listaRepository.InsertAsync(new Lista
            (
                id: Guid.Parse("e34ed866-71ee-4bce-afe1-c104dd4a482d"),
                titulo: "9b",
                enderecoId: null
            ));

            await _listaRepository.InsertAsync(new Lista
            (
                id: Guid.Parse("e04eb239-c7f3-454e-9149-f5c194a2546b"),
                titulo: "52",
                enderecoId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}