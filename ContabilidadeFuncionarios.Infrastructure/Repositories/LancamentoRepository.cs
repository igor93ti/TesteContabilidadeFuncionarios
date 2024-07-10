using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;
using ContabilidadeFuncionarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContabilidadeFuncionarios.Infrastructure.Repositories
{
    public class LancamentoRepository : Repository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Lancamento>> GetByFuncionarioIdAsync(int funcionarioId)
        {
            return await _context.Lancamentos
                .Where(l => l.FuncionarioId == funcionarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lancamento>> GetByFuncionarioAndMesAsync(int funcionarioId, DateTime mesReferencia)
        {
            return await _context.Lancamentos
                .Where(l => l.FuncionarioId == funcionarioId && l.DataLancamento.Month == mesReferencia.Month && l.DataLancamento.Year == mesReferencia.Year)
                .ToListAsync();
        }
    }
}
