using System.Threading.Tasks;

namespace ContabilidadeFuncionarios.Domain.Interfaces
{
    public interface ICalculoDescontoService
    {
        Task<decimal> CalcularINSS(decimal salarioBruto);
        Task<decimal> CalcularIRRF(decimal salarioBruto);
        Task<decimal> CalcularPlanoSaude(bool possuiPlanoSaude);
        Task<decimal> CalcularPlanoDental(bool possuiPlanoDental);
        Task<decimal> CalcularValeTransporte(bool possuiValeTransporte, decimal salarioBruto);
        Task<decimal> CalcularFGTS(decimal salarioBruto);
    }
}
