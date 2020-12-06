using API.Business.Interfaces;
using API.Models;
using API.Models.Validation;
using System;
using System.Threading.Tasks;

namespace API.Business.Services
{
    public class ExcelDataService : BaseService, IExcelDataService
    {
        private readonly IExcelDataRepository _excelDataRepository;
        public ExcelDataService(IExcelDataRepository excelDataRepository)
        {
            _excelDataRepository = excelDataRepository;
        }
        public async Task Adicionar(ExcelData excelData)
        {
            if (!ExecutarValidacao(new ExcelDataValidation(), excelData)) return;

            //var user = _user.GetUserId();

            await _excelDataRepository.Adicionar(excelData);
        }

        public async Task Atualizar(ExcelData excelData)
        {
            if (!ExecutarValidacao(new ExcelDataValidation(), excelData)) return;

            await _excelDataRepository.Atualizar(excelData);
        }

        public async Task Remover(Guid id)
        {
            await _excelDataRepository.Remover(id);
        }

        public void Dispose()
        {
            _excelDataRepository?.Dispose();
        }
    }
}
