using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace MaterialeShop.Listas
{
    public class ListaUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = ListaConsts.TituloMinLength)]
        public string Titulo { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}