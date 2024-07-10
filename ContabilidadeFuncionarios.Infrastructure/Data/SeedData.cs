using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Enums;
using ContabilidadeFuncionarios.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContabilidadeFuncionarios.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //if (context.TaxaDescontos.Any())
            //{
            //    return;
            //}

            var limiteMaximo = 999999999m;

            var taxaDescontos = new List<TaxaDesconto>
            {
                new(tipo: DescricaoLancamentoEnum.INSS, limiteInferior: 0m, limiteSuperior: 1045m, valor: 0.075m, deducao: 0m),
                new(tipo: DescricaoLancamentoEnum.INSS, limiteInferior: 1045.01m, limiteSuperior: 2089.60m, valor: 0.09m, deducao: 0m),
                new(tipo: DescricaoLancamentoEnum.INSS, limiteInferior: 2089.61m, limiteSuperior: 3134.40m, valor: 0.12m, deducao: 0m),
                new(tipo: DescricaoLancamentoEnum.INSS, limiteInferior: 3134.41m, limiteSuperior: 6101.06m, valor: 0.14m, deducao: 0m),
                new(tipo: DescricaoLancamentoEnum.IRRF, limiteInferior: 0m, limiteSuperior: 1903.98m, valor: 0m, deducao: 0m),
                new(tipo: DescricaoLancamentoEnum.IRRF, limiteInferior: 1903.99m, limiteSuperior: 2826.65m, valor: 0.075m, deducao: 142.8m),
                new(tipo: DescricaoLancamentoEnum.IRRF, limiteInferior: 2826.66m, limiteSuperior: 3751.05m, valor: 0.15m, deducao: 354.8m),
                new(tipo: DescricaoLancamentoEnum.IRRF, limiteInferior: 3751.06m, limiteSuperior: 4664.68m, valor: 0.225m, deducao: 636.13m),
                new(tipo: DescricaoLancamentoEnum.IRRF, limiteInferior: 4664.69m, limiteSuperior: limiteMaximo, valor: 0.275m, deducao: 869.36m),
                new(tipo: DescricaoLancamentoEnum.PlanoSaude, limiteInferior: 0m, limiteSuperior: limiteMaximo, valor: 10m, deducao: 0m),
                new(tipo: DescricaoLancamentoEnum.PlanoDental, limiteInferior: 0m, limiteSuperior: limiteMaximo, valor: 5m, deducao: 0m),
                new(tipo: DescricaoLancamentoEnum.ValeTransporte, limiteInferior: 0m, limiteSuperior: limiteMaximo, valor: 0.06m, deducao: 0m),
                new(tipo: DescricaoLancamentoEnum.FGTS, limiteInferior: 0m, limiteSuperior: limiteMaximo, valor: 0.08m, deducao: 0m),
            };

            context.TaxaDescontos.AddRange(taxaDescontos);
            context.SaveChanges();

            var funcionario = new Funcionario(
                Nome: "João",
                Sobrenome: "Silva",
                Documento: "12345678900",
                Setor: "TI",
                SalarioBruto: 5000m,
                DataAdmissao: DateTime.Now.AddYears(-1),
                PossuiPlanoSaude: true,
                PossuiPlanoDental: true,
                PossuiValeTransporte: true
            );

            context.Funcionarios.Add(funcionario);
            context.SaveChanges();

            var lancamentos = new List<Lancamento>
            {
                new(funcionarioId: funcionario.Id, dataLancamento: DateTime.Now.AddMonths(-1), valor: 5000m, descricao: "Salario"),
                new(funcionarioId: funcionario.Id, dataLancamento: DateTime.Now.AddMonths(-2), valor: 5000m, descricao: "Salario"),
                new(funcionarioId: funcionario.Id, dataLancamento: DateTime.Now.AddMonths(-3), valor: 5000m, descricao: "Salario")
            };

            context.Lancamentos.AddRange(lancamentos);
            context.SaveChanges();
        }
    }
}
