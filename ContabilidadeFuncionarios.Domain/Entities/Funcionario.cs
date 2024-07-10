using System;
using System.Collections.Generic;
using ContabilidadeFuncionarios.Domain.Models;

namespace ContabilidadeFuncionarios.Domain.Entities
{
    public class Funcionario(string Nome, string Sobrenome, string Documento, string Setor, decimal SalarioBruto, DateTime DataAdmissao, bool PossuiPlanoSaude, bool PossuiPlanoDental, bool PossuiValeTransporte)
    {
        public int Id { get; }
        public string Nome { get; set; } = Nome;
        public string Sobrenome { get; set; } = Sobrenome;
        public string Documento { get; set; } = Documento;
        public string Setor { get; set; } = Setor;
        public decimal SalarioBruto { get; set; } = SalarioBruto;
        public DateTime DataAdmissao { get; set; } = DataAdmissao;
        public bool PossuiPlanoSaude { get; set; } = PossuiPlanoSaude;
        public bool PossuiPlanoDental { get; set; } = PossuiPlanoDental;
        public bool PossuiValeTransporte { get; set; } = PossuiValeTransporte;

        public ICollection<Lancamento> Lancamentos { get; set; } = new List<Lancamento>();
    }
}
