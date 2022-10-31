using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace MaterialeShop.Listas
{
    public class ListaDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Titulo { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}