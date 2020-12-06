using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Lote : Entity
    {      
        public string DescricaoLoteArquivo { get; set; }
        public DateTime DataImportacao { get; set; }
        public int NumeroRegistros { get; set; }
        public int NumeroTotalProdutos { get; set; }
        public decimal ValorTotalImportado { get; set; }
        public DateTime DataEntregaMenor { get; set; }

        public virtual ICollection<ExcelData> ExcelDatas { get; set; }
    }
}
