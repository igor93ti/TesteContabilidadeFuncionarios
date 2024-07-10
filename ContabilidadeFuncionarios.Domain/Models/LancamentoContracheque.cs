using System;
using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Enums;

namespace ContabilidadeFuncionarios.Domain.Models
{
    public class LancamentoContracheque(string descricaoLancamento, string tipoLancamento, decimal Valor, DateTime DataLancamento)
    {
        public string DescricaoLacamento { get; set; } = descricaoLancamento;
        public string TipoLancamento { get; set; } = tipoLancamento;
        public decimal Valor { get; } = Valor;
        public DateTime DataLancamento { get; } = DataLancamento;
    }
}
