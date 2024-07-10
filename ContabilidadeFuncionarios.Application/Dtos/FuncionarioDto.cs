﻿namespace ContabilidadeFuncionarios.Application.DTOs
{
    public class FuncionarioDto(
        int Id,
        string Nome,
        string Sobrenome,
        string Documento,
        string Setor,
        decimal SalarioBruto,
        DateTime DataAdmissao,
        bool PossuiPlanoSaude,
        bool PossuiPlanoDental,
        bool PossuiValeTransporte)
    {
        public int Id { get; } = Id;
        public string Nome { get; } = Nome;
        public string Sobrenome { get; } = Sobrenome;
        public string Documento { get; } = Documento;
        public string Setor { get; } = Setor;
        public decimal SalarioBruto { get; } = SalarioBruto;
        public DateTime DataAdmissao { get; } = DataAdmissao;
        public bool PossuiPlanoSaude { get; } = PossuiPlanoSaude;
        public bool PossuiPlanoDental { get; } = PossuiPlanoDental;
        public bool PossuiValeTransporte { get; } = PossuiValeTransporte;
    }
}
