using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Enums;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;
using ContabilidadeFuncionarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadeFuncionarios.Infrastructure.Repositories
{
    public class TaxaDescontoRepository : Repository<TaxaDesconto>, ITaxaDescontoRepository
    {
        public TaxaDescontoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaxaDesconto>> GetByTipoAsync(DescricaoLancamentoEnum tipo)
        {
            return await _dbSet.Where(a => a.Tipo == tipo).ToListAsync();
        }
    }
}
