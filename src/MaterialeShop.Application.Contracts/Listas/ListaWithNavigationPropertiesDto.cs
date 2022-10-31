using MaterialeShop.Enderecos;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace MaterialeShop.Listas
{
    public class ListaWithNavigationPropertiesDto
    {
        public ListaDto Lista { get; set; }

        public EnderecoDto Endereco { get; set; }

    }
}