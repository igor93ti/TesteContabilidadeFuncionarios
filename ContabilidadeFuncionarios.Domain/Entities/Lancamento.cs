using ContabilidadeFuncionarios.Domain.Enums;
using System;

namespace ContabilidadeFuncionarios.Domain.Entities
{
    public class Lancamento(int funcionarioId, DateTime dataLancamento, decimal valor, string descricao)
    {
        public int Id { get; set; }
        public int FuncionarioId { get; set; } = funcionarioId;
        public DateTime DataLancamento { get; set; } = dataLancamento;
        public decimal Valor { get; set; } = valor;
        public string Descricao { get; set; } = descricao;
    }
}
