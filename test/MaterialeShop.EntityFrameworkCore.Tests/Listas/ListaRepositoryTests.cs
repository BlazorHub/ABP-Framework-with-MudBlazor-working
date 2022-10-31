using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using MaterialeShop.Listas;
using MaterialeShop.EntityFrameworkCore;
using Xunit;

namespace MaterialeShop.Listas
{
    public class ListaRepositoryTests : MaterialeShopEntityFrameworkCoreTestBase
    {
        private readonly IListaRepository _listaRepository;

        public ListaRepositoryTests()
        {
            _listaRepository = GetRequiredService<IListaRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _listaRepository.GetListAsync(
                    titulo: "c5"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _listaRepository.GetCountAsync(
                    titulo: "98"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}