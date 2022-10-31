using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MaterialeShop.Enderecos
{
    public class EnderecoCreateDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = EnderecoConsts.EnderecoCompletoMinLength)]
        public string EnderecoCompleto { get; set; }
    }
}