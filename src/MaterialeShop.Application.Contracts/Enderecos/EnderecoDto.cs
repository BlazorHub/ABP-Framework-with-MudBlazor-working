using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace MaterialeShop.Enderecos
{
    public class EnderecoDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string EnderecoCompleto { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}