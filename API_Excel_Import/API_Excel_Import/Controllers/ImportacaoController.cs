using API.Business.Interfaces;
using API.Data.Repository;
using API.Models;
using API.Models.Validation;
using API_Excel_Import.Extensions;
using API_Excel_Import.ViewModels;
using AutoMapper;
using ClosedXML.Excel;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Excel_Import.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ImportacaoController : ControllerBase
    {
        private readonly IExcelDataRepository _excelDataRepository;
        private readonly IExcelDataService _excelDataService;

        private readonly ILoteRepository _loteRepository;
        private readonly ILoteService _loteService;

        private readonly IMapper _mapper;

        public ImportacaoController(IExcelDataRepository excelDataRepository, IExcelDataService excelDataService, ILoteRepository loteRepository, ILoteService loteService, IMapper mapper)
        {
            _excelDataRepository = excelDataRepository;
            _excelDataService = excelDataService;
            _loteRepository = loteRepository;
            _loteService = loteService;
            _mapper = mapper;
        }



        //[HttpGet]
        //[Route("ImportCustomer")]
        //public IList<ExcelData> ImportExcel()
        //{

        //    string rootFolder = _hostingEnvironment.WebRootPath;
        //    string fileName = @"ImportCustomers.xlsx";
        //    FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

        //    using (ExcelPackage package = new ExcelPackage(file))
        //    {
        //        ExcelWorksheet workSheet = package.Workbook.Worksheets["Customer"];
        //        int totalRows = workSheet.Dimension.Rows;

        //        List<ExcelData> customerList = new List<ExcelData>();

        //        for (int i = 2; i <= totalRows; i++)
        //        {
        //            customerList.Add(new ExcelData
        //            {
        //                DataEntrega = Convert.ToDateTime(workSheet.Cells[i, 1].Value.ToString()),
        //                DescricaoProduto = workSheet.Cells[i, 2].Value.ToString(),
        //                QtdProduto = Convert.ToInt32(workSheet.Cells[i, 3].Value.ToString()),
        //                ValorUnitario = Convert.ToDecimal(workSheet.Cells[i, 4].Value.ToString())
        //            });
        //        }

        //        _repositoryExcelData.AdicionarDadosDoExcel(customerList);
        //        _repositoryExcelData.SaveChanges();

        //        return customerList;
        //    }
        //}
        [Route("api/importacao/getall")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<LoteViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<LoteViewModel>>(await _loteRepository.ObterTodos());
        }

        [Route("api/importacao/getbyid/{id:guid}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImportacaoExcelDataViewModel>> GetById(Guid id)
        {
            var entregas = await _excelDataRepository.ObterPorLoteId(id);

            if (entregas == null) return NotFound();
            
            return Ok(_mapper.Map<IEnumerable<ImportacaoExcelDataViewModel>>(entregas));
        }

        [Route("api/importacao/uploadexcel")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadExcel(ExcelViewModel excel)
        {

            var listaEntregas = DadosExcel(Convert.FromBase64String(excel.Base64));

            var lote = new Lote()
            {
                DescricaoLoteArquivo = excel.Nome,
                DataImportacao = DateTime.Now,
                NumeroRegistros = listaEntregas.Count,
                NumeroTotalProdutos = listaEntregas.Sum(e => e.QtdProduto),
                ValorTotalImportado = listaEntregas.Sum(e => e.ValorUnitario * e.QtdProduto),
                DataEntregaMenor = listaEntregas.Min(e => e.DataEntrega),
                ExcelDatas = listaEntregas,
            };

            await _loteService.Adicionar(_mapper.Map<Lote>(lote));
            await _loteRepository.SaveChanges();

            return Ok(new { success = true, data = excel });
        }

        [NonAction]
        private List<ExcelData> DadosExcel(byte[] buffer)
        {
            //CRIAR FLUXO CUJO REPOSITORIO DE BACKUP É A MEMORIA
            MemoryStream excelStream = new MemoryStream(buffer);
            var retorno = new List<ExcelData>();

            try
            {
                //WorkBook é o Excel que iremos Manipular 
                using var workBook = new XLWorkbook(excelStream);
                //WorkSheet são as varias planilhas que se pode ter dentro do WorkBook, neste caso estou setando como a primeira
                var workSheet = workBook.Worksheet(1);

                //Caso não haja informações na primeira planilha retorno a exception
                if (workSheet.Tables.Count() == 0)
                    throw new ExceptionMiddleware($"Não foram encontrados dados de entregas no arquivo.");

                var dtt = workSheet.Table(0).AsNativeDataTable();

                var colunasEncontradas = dtt.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToArray();
                var colunasEsperadas = new string[] { "Data Entrega", "Nome do Produto", "Quantidade", "Valor Unitário" };
                var nrColunasIguais = colunasEncontradas.Intersect(colunasEsperadas).Count();

                if (nrColunasIguais != colunasEsperadas.Count())
                    throw new ExceptionMiddleware($"O cabecalho do arquivo não condiz com o esperado. Verifique se os seguintes cabeçalhos estão presentes: {string.Join(", ", colunasEsperadas)}.");

                if (dtt.Rows.Count == 0)
                    throw new ExceptionMiddleware($"Não foram encontrados dados de entregas no arquivo.");

                var listErrors = new List<ValidationFailure>();
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    var validator = new ExcelDataValidation();
                    var row = dtt.Rows[i];

                    var dtEntrega = row.Field<DateTime>("Data Entrega");
                    var nmProduto = row.Field<string>("Nome do Produto");
                    var qtd = row.Field<double>("Quantidade");
                    var vlUnitario = Convert.ToDecimal(row.Field<double>("Valor Unitário"));

                    ExcelData data = new ExcelData()
                    {
                        DataEntrega = dtEntrega,
                        DescricaoProduto = nmProduto,
                        QtdProduto = Convert.ToInt32(qtd),
                        ValorUnitario = decimal.Round(vlUnitario, 2)
                    };

                    //Verifica se todos os pré-req estão sendo atendidos
                    var results = validator.Validate(data);

                    if (!results.IsValid)
                        listErrors.AddRange(results.Errors);

                    retorno.Add(data);
                }

                if (listErrors.Count > 0)
                    throw new ExceptionMiddleware("Foram encontrados dados fora das especificações.", listErrors);
            }
            catch (FileFormatException)
            {
                throw new ExceptionMiddleware("Formato inválido de arquivo.");
            }

            return retorno;
        }
    }
}
