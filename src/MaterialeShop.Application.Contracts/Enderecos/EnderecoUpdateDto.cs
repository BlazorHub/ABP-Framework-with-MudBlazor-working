using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace MaterialeShop.Enderecos
{
    public class EnderecoUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = EnderecoConsts.EnderecoCompletoMinLength)]
        public string EnderecoCompleto { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}