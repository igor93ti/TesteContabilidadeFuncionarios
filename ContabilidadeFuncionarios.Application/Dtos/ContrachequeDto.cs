using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContabilidadeFuncionarios.Application.Dtos
{
    public class ContrachequeDto(string MesReferencia, decimal SalarioBruto, decimal TotalDescontos, decimal SalarioLiquido)
    {
        public string MesReferencia { get; } = MesReferencia;
        public decimal SalarioBruto { get; } = SalarioBruto;
        public decimal TotalDescontos { get; } = TotalDescontos;
        public decimal SalarioLiquido { get; } = SalarioLiquido;
        public List<LancamentoDto> Lancamentos { get; } = new List<LancamentoDto>();
    }
}
