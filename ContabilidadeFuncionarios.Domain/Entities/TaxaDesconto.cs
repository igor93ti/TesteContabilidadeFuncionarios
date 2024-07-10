using ContabilidadeFuncionarios.Domain.Enums;

namespace ContabilidadeFuncionarios.Domain.Entities
{
    public class TaxaDesconto(DescricaoLancamentoEnum tipo, decimal limiteInferior, decimal limiteSuperior, decimal valor, decimal deducao)
    {
        public int Id { get; set; }
        public DescricaoLancamentoEnum Tipo { get; set; } = tipo;
        public decimal LimiteInferior { get; set; } = limiteInferior;
        public decimal LimiteSuperior { get; set; } = limiteSuperior;
        public decimal Valor { get; set; } = valor;
        public decimal Deducao { get; set; } = deducao;
    }
}
