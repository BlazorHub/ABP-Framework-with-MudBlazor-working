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
            result.Items.Any(x => x.Id == Guid.Parse("ca05d424-149b-4f00-8724-68fed5686811")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("f4e6bff8-974f-4d6e-bde5-a2baea10cd9e")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _listaItemsAppService.GetAsync(Guid.Parse("ca05d424-149b-4f00-8724-68fed5686811"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ca05d424-149b-4f00-8724-68fed5686811"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ListaItemCreateDto
            {
                Descricao = "b8",
                Quantidade = "7b5902d716044",
                UnidadeMedida = "ca8fc05621c141bea2ebb1dfa428c0ce5dc461aa8f2"
            };

            // Act
            var serviceResult = await _listaItemsAppService.CreateAsync(input);

            // Assert
            var result = await _listaItemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Descricao.ShouldBe("b8");
            result.Quantidade.ShouldBe("7b5902d716044");
            result.UnidadeMedida.ShouldBe("ca8fc05621c141bea2ebb1dfa428c0ce5dc461aa8f2");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ListaItemUpdateDto()
            {
                Descricao = "af",
                Quantidade = "900456f5398e4c568d481977e45f3b6bdb7f4ff248cf431fb79e161ad",
                UnidadeMedida = "6819786529c34c0d95d6ca52e837addbd706b44017964991a201cf5ce321d0e"
            };

            // Act
            var serviceResult = await _listaItemsAppService.UpdateAsync(Guid.Parse("ca05d424-149b-4f00-8724-68fed5686811"), input);

            // Assert
            var result = await _listaItemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Descricao.ShouldBe("af");
            result.Quantidade.ShouldBe("900456f5398e4c568d481977e45f3b6bdb7f4ff248cf431fb79e161ad");
            result.UnidadeMedida.ShouldBe("6819786529c34c0d95d6ca52e837addbd706b44017964991a201cf5ce321d0e");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _listaItemsAppService.DeleteAsync(Guid.Parse("ca05d424-149b-4f00-8724-68fed5686811"));

            // Assert
            var result = await _listaItemRepository.FindAsync(c => c.Id == Guid.Parse("ca05d424-149b-4f00-8724-68fed5686811"));

            result.ShouldBeNull();
        }
    }
}