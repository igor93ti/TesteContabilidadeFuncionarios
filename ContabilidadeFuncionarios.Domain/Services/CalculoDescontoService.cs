using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Enums;
using ContabilidadeFuncionarios.Domain.Interfaces;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;

namespace ContabilidadeFuncionarios.Domain.Services
{
    public class CalculoDescontoService : ICalculoDescontoService
    {
        private readonly ITaxaDescontoRepository _taxaDescontoRepository;

        public CalculoDescontoService(ITaxaDescontoRepository taxaDescontoRepository)
        {
            _taxaDescontoRepository = taxaDescontoRepository;
        }

        public async Task<decimal> CalcularINSS(decimal salarioBruto)
        {
            var faixasINSS = (await _taxaDescontoRepository.GetByTipoAsync(DescricaoLancamentoEnum.INSS))
                             .OrderByDescending(f => f.LimiteSuperior);

            foreach (var faixa in faixasINSS)
            {
                if (salarioBruto > faixa.LimiteInferior)
                {
                    if(salarioBruto > faixa.LimiteSuperior)
                    {
                        return faixa.LimiteSuperior * faixa.Valor;
                    }
                    else
                    {
                        return salarioBruto * faixa.Valor;
                    }
                }
            }

            return 0;
        }

        public async Task<decimal> CalcularIRRF(decimal salarioBruto)
        {
            var faixasIRRF = (await _taxaDescontoRepository.GetByTipoAsync(DescricaoLancamentoEnum.IRRF))
                             .OrderByDescending(f => f.LimiteSuperior);

            foreach (var faixa in faixasIRRF)
            {
                if (salarioBruto > faixa.LimiteInferior)
                {
                    var irrf = salarioBruto * faixa.Valor;
                    return Math.Min(irrf, faixa.Deducao);
                }
            }

            return 0;
        }

        public async Task<decimal> CalcularPlanoSaude(bool possuiPlanoSaude)
        {
            if (!possuiPlanoSaude) return 0;
            var planoSaude = await _taxaDescontoRepository.GetByTipoAsync(DescricaoLancamentoEnum.PlanoSaude);
            var taxa = planoSaude.FirstOrDefault();
            if (taxa == null) throw new Exception("Taxa do Plano de Saúde não encontrada.");
            return taxa.Valor;
        }

        public async Task<decimal> CalcularPlanoDental(bool possuiPlanoDental)
        {
            if (!possuiPlanoDental) return 0;
            var planoDental = await _taxaDescontoRepository.GetByTipoAsync(DescricaoLancamentoEnum.PlanoDental);
            var taxa = planoDental.FirstOrDefault();
            if (taxa == null) throw new Exception("Taxa do Plano Dental não encontrada.");
            return taxa.Valor;
        }

        public async Task<decimal> CalcularValeTransporte(bool possuiValeTransporte, decimal salarioBruto)
        {
            if (!possuiValeTransporte) return 0;
            var valeTransporte = await _taxaDescontoRepository.GetByTipoAsync(DescricaoLancamentoEnum.ValeTransporte);
            var taxa = valeTransporte.FirstOrDefault();
            if (taxa == null) throw new Exception("Taxa do Vale Transporte não encontrada.");
            return salarioBruto * taxa.Valor;
        }

        public async Task<decimal> CalcularFGTS(decimal salarioBruto)
        {
            var fgts = await _taxaDescontoRepository.GetByTipoAsync(DescricaoLancamentoEnum.FGTS);
            var taxa = fgts.FirstOrDefault();
            if (taxa == null) throw new Exception("Taxa de FGTS não encontrada.");
            return salarioBruto * taxa.Valor;
        }
    }
}
