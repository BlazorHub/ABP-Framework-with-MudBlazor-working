using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using MaterialeShop.Enderecos;

namespace MaterialeShop.Enderecos
{
    public class EnderecosDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public EnderecosDataSeedContributor(IEnderecoRepository enderecoRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _enderecoRepository = enderecoRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _enderecoRepository.InsertAsync(new Endereco
            (
                id: Guid.Parse("6cadc905-018f-4c24-a1f1-d6d2f4de2132"),
                enderecoCompleto: "48"
            ));

            await _enderecoRepository.InsertAsync(new Endereco
            (
                id: Guid.Parse("771eb7b1-0336-40a4-88bd-a5ea4fbabae4"),
                enderecoCompleto: "99"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}