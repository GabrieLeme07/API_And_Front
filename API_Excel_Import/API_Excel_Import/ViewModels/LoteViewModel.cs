using Newtonsoft.Json;
using System;

namespace API_Excel_Import.ViewModels
{
    public class LoteViewModel
    {
        public Guid Id { get; set; }

        [JsonProperty("DataImportacao")]
        public DateTime DataImportacao { get; set; }

        [JsonProperty("NumeroRegistros")]
        public int NumeroRegistros { get; set; }

        [JsonProperty("NumeroTotalProdutos")]
        public int NumeroTotalProdutos { get; set; }

        [JsonProperty("ValorTotal")]
        public decimal ValorTotalImportado { get; set; }

        public DateTime MenorDataEntrega { get; set; }
    }
}
