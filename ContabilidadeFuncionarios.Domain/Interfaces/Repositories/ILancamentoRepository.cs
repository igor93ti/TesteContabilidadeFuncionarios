using ContabilidadeFuncionarios.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadeFuncionarios.Domain.Interfaces.Repositories
{
    public interface ILancamentoRepository : IRepository<Lancamento>
    {
        Task<IEnumerable<Lancamento>> GetByFuncionarioIdAsync(int funcionarioId);

        Task<IEnumerable<Lancamento>> GetByFuncionarioAndMesAsync(int funcionarioId, DateTime mesReferencia);
    }
}
