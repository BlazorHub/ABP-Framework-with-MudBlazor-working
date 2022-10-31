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
            result.Items.Any(x => x.Id == Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("ce235555-ad62-4b61-8f7f-2f45117f8d8d")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _listasAppService.GetAsync(Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ListaCreateDto
            {
                Titulo = "6f"
            };

            // Act
            var serviceResult = await _listasAppService.CreateAsync(input);

            // Assert
            var result = await _listaRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Titulo.ShouldBe("6f");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ListaUpdateDto()
            {
                Titulo = "29"
            };

            // Act
            var serviceResult = await _listasAppService.UpdateAsync(Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00"), input);

            // Assert
            var result = await _listaRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Titulo.ShouldBe("29");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _listasAppService.DeleteAsync(Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00"));

            // Assert
            var result = await _listaRepository.FindAsync(c => c.Id == Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00"));

            result.ShouldBeNull();
        }
    }
}