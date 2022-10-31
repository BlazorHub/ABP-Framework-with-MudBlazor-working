using Volo.Abp.Application.Dtos;
using System;

namespace MaterialeShop.ListaItems
{
    public class ListaItemExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Descricao { get; set; }
        public string Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public Guid? ListaId { get; set; }

        public ListaItemExcelDownloadDto()
        {

        }
    }
}