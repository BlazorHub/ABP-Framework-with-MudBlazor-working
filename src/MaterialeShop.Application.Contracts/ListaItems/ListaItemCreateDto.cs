using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MaterialeShop.ListaItems
{
    public class ListaItemCreateDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = ListaItemConsts.DescricaoMinLength)]
        public string Descricao { get; set; }
        public string Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public Guid ListaId { get; set; }
    }
}