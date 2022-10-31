using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MaterialeShop.Listas
{
    public class ListasAppServiceTests : MaterialeShopApplicationTestBase
    {
        private readonly IListasAppService _listasAppService;
        private readonly IRepository<Lista, Guid> _listaRepository;

        public ListasAppServiceTests()
        {
            _listasAppService = GetRequiredService<IListasAppService>();
            _listaRepository = GetRequiredService<IRepository<Lista, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _listasAppService.GetListAsync(new GetListasInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Lista.Id == Guid.Parse("e34ed866-71ee-4bce-afe1-c104dd4a482d")).ShouldBe(true);
            result.Items.Any(x => x.Lista.Id == Guid.Parse("e04eb239-c7f3-454e-9149-f5c194a2546b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _listasAppService.GetAsync(Guid.Parse("e34ed866-71ee-4bce-afe1-c104dd4a482d"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e34ed866-71ee-4bce-afe1-c104dd4a482d"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ListaCreateDto
            {
                Titulo = "b7"
            };

            // Act
            var serviceResult = await _listasAppService.CreateAsync(input);

            // Assert
            var result = await _listaRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Titulo.ShouldBe("b7");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ListaUpdateDto()
            {
                Titulo = "67"
            };

            // Act
            var serviceResult = await _listasAppService.UpdateAsync(Guid.Parse("e34ed866-71ee-4bce-afe1-c104dd4a482d"), input);

            // Assert
            var result = await _listaRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Titulo.ShouldBe("67");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _listasAppService.DeleteAsync(Guid.Parse("e34ed866-71ee-4bce-afe1-c104dd4a482d"));

            // Assert
            var result = await _listaRepository.FindAsync(c => c.Id == Guid.Parse("e34ed866-71ee-4bce-afe1-c104dd4a482d"));

            result.ShouldBeNull();
        }
    }
}