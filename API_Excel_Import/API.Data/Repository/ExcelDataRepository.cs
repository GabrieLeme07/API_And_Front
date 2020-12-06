using API.Business.Interfaces;
using API.Data.Context;
using API.Models;

namespace API.Data.Repository
{
    public  class ExcelDataRepository : Repository<ExcelData>, IExcelDataRepository
    {
        public ExcelDataRepository(MeuDbContext context) : base(context) { }
    }
}

