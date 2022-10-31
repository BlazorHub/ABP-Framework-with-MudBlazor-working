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

        public ListasDataSeedContributor(IListaRepository listaRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _listaRepository = listaRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _listaRepository.InsertAsync(new Lista
            (
                id: Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00"),
                titulo: "c5"
            ));

            await _listaRepository.InsertAsync(new Lista
            (
                id: Guid.Parse("ce235555-ad62-4b61-8f7f-2f45117f8d8d"),
                titulo: "98"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}