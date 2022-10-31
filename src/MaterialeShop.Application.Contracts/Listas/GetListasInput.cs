using Volo.Abp.Application.Dtos;
using System;

namespace MaterialeShop.Listas
{
    public class GetListasInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Titulo { get; set; }

        public GetListasInput()
        {

        }
    }
}