using API.Business.Interfaces;
using API.Data.Context;
using API.Models;

namespace API.Data.Repository
{
    public class LoteRepository : Repository<Lote>, ILoteRepository
    {
        public LoteRepository(MeuDbContext context) : base(context) { }
    }
}
