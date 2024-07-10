using System;
using System.Threading.Tasks;
using ContabilidadeFuncionarios.Domain;

namespace ContabilidadeFuncionarios.Domain.Interfaces
{
    public interface IContrachequeService
    {
        Task<Contracheque> GerarContrachequeAsync(int funcionarioId, DateTime mesReferencia);
    }
}
