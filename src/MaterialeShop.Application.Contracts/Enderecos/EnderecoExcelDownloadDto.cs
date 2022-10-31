using Volo.Abp.Application.Dtos;
using System;

namespace MaterialeShop.Enderecos
{
    public class EnderecoExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string EnderecoCompleto { get; set; }

        public EnderecoExcelDownloadDto()
        {

        }
    }
}