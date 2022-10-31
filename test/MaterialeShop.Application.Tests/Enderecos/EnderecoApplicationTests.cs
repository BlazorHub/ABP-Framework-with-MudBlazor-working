using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MaterialeShop.Enderecos
{
    public class EnderecosAppServiceTests : MaterialeShopApplicationTestBase
    {
        private readonly IEnderecosAppService _enderecosAppService;
        private readonly IRepository<Endereco, Guid> _enderecoRepository;

        public EnderecosAppServiceTests()
        {
            _enderecosAppService = GetRequiredService<IEnderecosAppService>();
            _enderecoRepository = GetRequiredService<IRepository<Endereco, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _enderecosAppService.GetListAsync(new GetEnderecosInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("6cadc905-018f-4c24-a1f1-d6d2f4de2132")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("771eb7b1-0336-40a4-88bd-a5ea4fbabae4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _enderecosAppService.GetAsync(Guid.Parse("6cadc905-018f-4c24-a1f1-d6d2f4de2132"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6cadc905-018f-4c24-a1f1-d6d2f4de2132"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EnderecoCreateDto
            {
                EnderecoCompleto = "40"
            };

            // Act
            var serviceResult = await _enderecosAppService.CreateAsync(input);

            // Assert
            var result = await _enderecoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnderecoCompleto.ShouldBe("40");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EnderecoUpdateDto()
            {
                EnderecoCompleto = "17"
            };

            // Act
            var serviceResult = await _enderecosAppService.UpdateAsync(Guid.Parse("6cadc905-018f-4c24-a1f1-d6d2f4de2132"), input);

            // Assert
            var result = await _enderecoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EnderecoCompleto.ShouldBe("17");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _enderecosAppService.DeleteAsync(Guid.Parse("6cadc905-018f-4c24-a1f1-d6d2f4de2132"));

            // Assert
            var result = await _enderecoRepository.FindAsync(c => c.Id == Guid.Parse("6cadc905-018f-4c24-a1f1-d6d2f4de2132"));

            result.ShouldBeNull();
        }
    }
}