using System.ComponentModel.DataAnnotations;

namespace API_Excel_Import.ViewModels
{
    public class ExcelViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Base64 { get; set; }
    }
}
