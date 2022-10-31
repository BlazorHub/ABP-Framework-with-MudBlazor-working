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
                    titulo: "9b"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("e34ed866-71ee-4bce-afe1-c104dd4a482d"));
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
                    titulo: "52"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}