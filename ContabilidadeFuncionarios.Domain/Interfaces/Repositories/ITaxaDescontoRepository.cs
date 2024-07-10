using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Enums;
using System.Threading.Tasks;

namespace ContabilidadeFuncionarios.Domain.Interfaces.Repositories
{
    public interface ITaxaDescontoRepository : IRepository<TaxaDesconto>
    {
        Task<IEnumerable<TaxaDesconto>> GetByTipoAsync(DescricaoLancamentoEnum tipo);
    }
}
