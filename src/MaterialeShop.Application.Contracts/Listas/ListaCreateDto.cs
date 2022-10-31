using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MaterialeShop.Listas
{
    public class ListaCreateDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = ListaConsts.TituloMinLength)]
        public string Titulo { get; set; }
        public Guid? EnderecoId { get; set; }
    }
}