using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using MaterialeShop.Enderecos;
using MaterialeShop.EntityFrameworkCore;
using Xunit;

namespace MaterialeShop.Enderecos
{
    public class EnderecoRepositoryTests : MaterialeShopEntityFrameworkCoreTestBase
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoRepositoryTests()
        {
            _enderecoRepository = GetRequiredService<IEnderecoRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _enderecoRepository.GetListAsync(
                    enderecoCompleto: "48"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6cadc905-018f-4c24-a1f1-d6d2f4de2132"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _enderecoRepository.GetCountAsync(
                    enderecoCompleto: "99"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}