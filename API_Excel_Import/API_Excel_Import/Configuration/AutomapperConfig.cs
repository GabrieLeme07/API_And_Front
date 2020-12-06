using API.Models;
using API_Excel_Import.ViewModels;
using AutoMapper;

namespace API_Excel_Import.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ImportacaoExcelDataViewModel, ExcelData>().ReverseMap();
            CreateMap<LoteViewModel, Lote>()
              .ForMember(o => o.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(o => o.DataEntregaMenor, opt => opt.MapFrom(src => src.MenorDataEntrega))
              .ReverseMap();
        }
    }
}
