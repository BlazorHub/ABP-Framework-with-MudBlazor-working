using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using MaterialeShop.ListaItems;
using MaterialeShop.EntityFrameworkCore;
using Xunit;

namespace MaterialeShop.ListaItems
{
    public class ListaItemRepositoryTests : MaterialeShopEntityFrameworkCoreTestBase
    {
        private readonly IListaItemRepository _listaItemRepository;

        public ListaItemRepositoryTests()
        {
            _listaItemRepository = GetRequiredService<IListaItemRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _listaItemRepository.GetListAsync(
                    descricao: "a5",
                    quantidade: "d67e6cf6ff9a46a5b252c7c2ff9941e8b0de7af9566e42969abdcfca24edd0fc03a212a28d3",
                    unidadeMedida: "be3f40da0cba497fb2e23a2f34a348a898cd7f"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("ca05d424-149b-4f00-8724-68fed5686811"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _listaItemRepository.GetCountAsync(
                    descricao: "06",
                    quantidade: "bed606c2bc9e476db9b41ed6ba00ce0ea3bd3f3580f942c29eded8e2df3cff20327d183568b6404aa0e7",
                    unidadeMedida: "4a0fe57a8f404dc8a27026563372a4210433d458e76c43e29e3ec7243473c15e146f0046486b4e5"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}