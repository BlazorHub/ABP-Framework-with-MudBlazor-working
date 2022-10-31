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
                    descricao: "65",
                    quantidade: "c0319b70901",
                    unidadeMedida: "9e0ef6c1517a453c847ddefb69275fe5"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("1bac6082-4543-4c7e-9ef8-5e45262904b1"));
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
                    descricao: "65",
                    quantidade: "9f07dcce1a554b8f8cff869f7dfc018e1a41f3ea48ac4ecab895626fe99df971b2bb",
                    unidadeMedida: "9ebeec66872749538c53eca54e4c7cbeb19e0210cacc4712831154664afda57576a66727ff184e23b908d44499"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}