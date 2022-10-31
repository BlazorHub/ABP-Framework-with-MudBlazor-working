using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MaterialeShop.ListaItems
{
    public class ListaItemsAppServiceTests : MaterialeShopApplicationTestBase
    {
        private readonly IListaItemsAppService _listaItemsAppService;
        private readonly IRepository<ListaItem, Guid> _listaItemRepository;

        public ListaItemsAppServiceTests()
        {
            _listaItemsAppService = GetRequiredService<IListaItemsAppService>();
            _listaItemRepository = GetRequiredService<IRepository<ListaItem, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _listaItemsAppService.GetListAsync(new GetListaItemsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ListaItem.Id == Guid.Parse("1bac6082-4543-4c7e-9ef8-5e45262904b1")).ShouldBe(true);
            result.Items.Any(x => x.ListaItem.Id == Guid.Parse("6f05507b-7a4d-4690-b72e-4718d5d4be7a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _listaItemsAppService.GetAsync(Guid.Parse("1bac6082-4543-4c7e-9ef8-5e45262904b1"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1bac6082-4543-4c7e-9ef8-5e45262904b1"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ListaItemCreateDto
            {
                Descricao = "64",
                Quantidade = "d10314a2c5e8421ba47d6e7fdd701d8d9914568cb87c40a88bc29527b9",
                UnidadeMedida = "4e761cca39044c378ef6e75cb390f2d60bf29c9b42db49c5b7a4917581d8007585971ebfc2e",
                ListaId = Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00")
            };

            // Act
            var serviceResult = await _listaItemsAppService.CreateAsync(input);

            // Assert
            var result = await _listaItemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Descricao.ShouldBe("64");
            result.Quantidade.ShouldBe("d10314a2c5e8421ba47d6e7fdd701d8d9914568cb87c40a88bc29527b9");
            result.UnidadeMedida.ShouldBe("4e761cca39044c378ef6e75cb390f2d60bf29c9b42db49c5b7a4917581d8007585971ebfc2e");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ListaItemUpdateDto()
            {
                Descricao = "74",
                Quantidade = "d4ff02e50e3248d9a6932b17b610e530b1487a19d4354490a69a1f9f10a9490c1e654f3db49",
                UnidadeMedida = "2d861984c74b4833b7b32d41cf6ca05d94eaf78c3b0449a8abae071bc9ab6ad3837ac18e1e6447d9b02cd5",
                ListaId = Guid.Parse("b3dd3f22-eaa8-44b6-8306-aa1e77c0cf00")
            };

            // Act
            var serviceResult = await _listaItemsAppService.UpdateAsync(Guid.Parse("1bac6082-4543-4c7e-9ef8-5e45262904b1"), input);

            // Assert
            var result = await _listaItemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Descricao.ShouldBe("74");
            result.Quantidade.ShouldBe("d4ff02e50e3248d9a6932b17b610e530b1487a19d4354490a69a1f9f10a9490c1e654f3db49");
            result.UnidadeMedida.ShouldBe("2d861984c74b4833b7b32d41cf6ca05d94eaf78c3b0449a8abae071bc9ab6ad3837ac18e1e6447d9b02cd5");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _listaItemsAppService.DeleteAsync(Guid.Parse("1bac6082-4543-4c7e-9ef8-5e45262904b1"));

            // Assert
            var result = await _listaItemRepository.FindAsync(c => c.Id == Guid.Parse("1bac6082-4543-4c7e-9ef8-5e45262904b1"));

            result.ShouldBeNull();
        }
    }
}