using System;
using System.Collections.Generic;
using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Models;

namespace ContabilidadeFuncionarios.Domain
{
    public class Contracheque(DateTime mesReferencia, List<LancamentoContracheque> lancamentos, decimal salarioBruto, decimal totalDescontos, decimal salarioLiquido)
    {
        public DateTime MesReferencia { get; } = mesReferencia;
        public List<LancamentoContracheque> Lancamentos { get; } = lancamentos;
        public decimal SalarioBruto { get; } = salarioBruto;
        public decimal TotalDescontos { get; } = totalDescontos;
        public decimal SalarioLiquido { get; } = salarioLiquido;
    }
}
