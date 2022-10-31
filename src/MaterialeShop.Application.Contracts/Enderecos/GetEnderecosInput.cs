using Volo.Abp.Application.Dtos;
using System;

namespace MaterialeShop.Enderecos
{
    public class GetEnderecosInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string EnderecoCompleto { get; set; }

        public GetEnderecosInput()
        {

        }
    }
}