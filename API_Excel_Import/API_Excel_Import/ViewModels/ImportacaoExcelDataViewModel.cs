using System;

namespace API_Excel_Import.ViewModels
{
    public class ImportacaoExcelDataViewModel
    {
        public DateTime DataEntrega { get; set; }

        public string DescricaoProduto { get; set; }

        public decimal ValorUnitario { get; set; }

        public int QtdProduto { get; set; }

        public decimal VlTotal => ValorUnitario * QtdProduto;
    }
}
