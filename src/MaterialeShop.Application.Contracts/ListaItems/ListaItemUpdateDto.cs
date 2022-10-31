using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace MaterialeShop.ListaItems
{
    public class ListaItemUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = ListaItemConsts.DescricaoMinLength)]
        public string Descricao { get; set; }
        public string Quantidade { get; set; }
        public string UnidadeMedida { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}