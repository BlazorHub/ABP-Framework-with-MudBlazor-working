using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using MaterialeShop.ListaItems;

namespace MaterialeShop.ListaItems
{
    public class ListaItemsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IListaItemRepository _listaItemRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ListaItemsDataSeedContributor(IListaItemRepository listaItemRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _listaItemRepository = listaItemRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _listaItemRepository.InsertAsync(new ListaItem
            (
                id: Guid.Parse("ca05d424-149b-4f00-8724-68fed5686811"),
                descricao: "a5",
                quantidade: "d67e6cf6ff9a46a5b252c7c2ff9941e8b0de7af9566e42969abdcfca24edd0fc03a212a28d3",
                unidadeMedida: "be3f40da0cba497fb2e23a2f34a348a898cd7f"
            ));

            await _listaItemRepository.InsertAsync(new ListaItem
            (
                id: Guid.Parse("f4e6bff8-974f-4d6e-bde5-a2baea10cd9e"),
                descricao: "06",
                quantidade: "bed606c2bc9e476db9b41ed6ba00ce0ea3bd3f3580f942c29eded8e2df3cff20327d183568b6404aa0e7",
                unidadeMedida: "4a0fe57a8f404dc8a27026563372a4210433d458e76c43e29e3ec7243473c15e146f0046486b4e5"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}