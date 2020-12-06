using API.Models;
using System;
using System.Threading.Tasks;

namespace API.Business.Interfaces
{
    public interface ILoteService : IDisposable
    {
        Task Adicionar(Lote lote);
        Task Atualizar(Lote lote);
        Task Remover(Guid id);
    }
}
