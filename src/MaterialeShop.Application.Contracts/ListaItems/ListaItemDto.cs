using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace MaterialeShop.ListaItems
{
    public class ListaItemDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Descricao { get; set; }
        public string Quantidade { get; set; }
        public string UnidadeMedida { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}