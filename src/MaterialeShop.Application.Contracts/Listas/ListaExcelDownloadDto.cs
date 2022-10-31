using Volo.Abp.Application.Dtos;
using System;

namespace MaterialeShop.Listas
{
    public class ListaExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Titulo { get; set; }

        public ListaExcelDownloadDto()
        {

        }
    }
}