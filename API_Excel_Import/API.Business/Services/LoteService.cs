using API.Business.Interfaces;
using API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Business.Services
{
    public class LoteService : BaseService ,ILoteService
    {
        private readonly ILoteRepository _loteRepository;

        public LoteService(ILoteRepository loteRepository)
        {
            _loteRepository = loteRepository;
        }
        public async Task Adicionar(Lote lote)
        {
            //var user = _user.GetUserId();

            await _loteRepository.Adicionar(lote);
        }

        public async Task Atualizar(Lote lote)
        {
            await _loteRepository.Atualizar(lote);
        }

        public async Task Remover(Guid id)
        {
            await _loteRepository.Remover(id);
        }

        public void Dispose()
        {
            _loteRepository?.Dispose();
        }
    }
}
