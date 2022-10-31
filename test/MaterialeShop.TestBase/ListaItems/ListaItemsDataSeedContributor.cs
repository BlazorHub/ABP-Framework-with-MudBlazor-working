using MaterialeShop.Listas;
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
        private readonly ListasDataSeedContributor _listasDataSeedContributor;

        public ListaItemsDataSeedContributor(IListaItemRepository listaItemRepository, IUnitOfWorkManager unitOfWorkManager, ListasDataSeedContributor listasDataSeedContributor)
        {
            _listaItemRepository = listaItemRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _listasDataSeedContributor = listasDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _listasDataSeedContributor.SeedAsync(context);

            await _listaItemRepository.InsertAsync(new ListaItem
            (
                id: Guid.Parse("1bac6082-4543-4c7e-9ef8-5e45262904b1"),
                descricao: "65",
                quantidade: "c0319b70901",
                unidadeMedida: "9e0ef6c1517a453c847ddefb69275fe5",
                listaId: Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00")
            ));

            await _listaItemRepository.InsertAsync(new ListaItem
            (
                id: Guid.Parse("6f05507b-7a4d-4690-b72e-4718d5d4be7a"),
                descricao: "65",
                quantidade: "9f07dcce1a554b8f8cff869f7dfc018e1a41f3ea48ac4ecab895626fe99df971b2bb",
                unidadeMedida: "9ebeec66872749538c53eca54e4c7cbeb19e0210cacc4712831154664afda57576a66727ff184e23b908d44499",
                listaId: Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}