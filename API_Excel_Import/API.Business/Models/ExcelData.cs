using System;

namespace API.Models
{
    public class ExcelData : Entity
    {
        public Guid IdLoteEntrega { get; set; }
        public DateTime DataEntrega { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        public int QtdProduto { get; set; }

        public virtual Lote Lote { get; set; }


    }
}
