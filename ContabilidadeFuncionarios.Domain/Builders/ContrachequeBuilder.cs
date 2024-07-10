using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Enums;
using ContabilidadeFuncionarios.Domain.Interfaces;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;
using ContabilidadeFuncionarios.Domain.Models;

namespace ContabilidadeFuncionarios.Domain.Builders
{
    public class ContrachequeBuilder
    {
        private readonly Funcionario _funcionario;
        private readonly DateTime _mesReferencia;
        private readonly ICalculoDescontoService _calculoDescontoService;
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly List<LancamentoContracheque> _lancamentos;
        private decimal? _totalRemuneracoes;

        public ContrachequeBuilder(Funcionario funcionario, DateTime mesReferencia, ICalculoDescontoService calculoDescontoService, ILancamentoRepository lancamentoRepository)
        {
            _funcionario = funcionario;
            _mesReferencia = mesReferencia;
            _calculoDescontoService = calculoDescontoService;
            _lancamentoRepository = lancamentoRepository;
            _lancamentos = new List<LancamentoContracheque>();
        }

        private decimal CalcularTotalRemuneracoes()
        {
            if (_totalRemuneracoes == null)
            {
                _totalRemuneracoes = _lancamentos
                    .Where(l => l.TipoLancamento == TipoLancamentoEnum.Remuneracao.ToString())
                    .Sum(l => l.Valor);
            }
            return _totalRemuneracoes.Value;
        }

        public async Task<ContrachequeBuilder> AdicionarLancamentoAsync(string descricao, string tipoLancamento, decimal valor, DateTime dataLancamento)
        {
            var lancamento = new LancamentoContracheque(descricao, tipoLancamento, valor, dataLancamento);
            _lancamentos.Add(lancamento);
            return await Task.FromResult(this);
        }

        public async Task<ContrachequeBuilder> AdicionarDescontoINSSAsync()
        {
            decimal totalRemuneracoes = CalcularTotalRemuneracoes();
            decimal valor = await _calculoDescontoService.CalcularINSS(totalRemuneracoes);
            return await AdicionarLancamentoAsync(DescricaoLancamentoEnum.INSS.ToString(), TipoLancamentoEnum.Desconto.ToString(), valor, _mesReferencia);
        }

        public async Task<ContrachequeBuilder> AdicionarDescontoIRRFAsync()
        {
            decimal totalRemuneracoes = CalcularTotalRemuneracoes();
            decimal valor = await _calculoDescontoService.CalcularIRRF(totalRemuneracoes);
            return await AdicionarLancamentoAsync(DescricaoLancamentoEnum.IRRF.ToString(), TipoLancamentoEnum.Desconto.ToString(), valor, _mesReferencia);
        }

        public async Task<ContrachequeBuilder> AdicionarDescontoPlanoSaudeAsync()
        {
            decimal valor = await _calculoDescontoService.CalcularPlanoSaude(_funcionario.PossuiPlanoSaude);
            return await AdicionarLancamentoAsync(DescricaoLancamentoEnum.PlanoSaude.ToString(), TipoLancamentoEnum.Desconto.ToString(), valor, _mesReferencia);
        }

        public async Task<ContrachequeBuilder> AdicionarDescontoPlanoDentalAsync()
        {
            decimal valor = await _calculoDescontoService.CalcularPlanoDental(_funcionario.PossuiPlanoDental);
            return await AdicionarLancamentoAsync(DescricaoLancamentoEnum.PlanoDental.ToString(), TipoLancamentoEnum.Desconto.ToString(), valor, _mesReferencia);
        }

        public async Task<ContrachequeBuilder> AdicionarDescontoValeTransporteAsync()
        {
            decimal totalRemuneracoes = CalcularTotalRemuneracoes();
            decimal valor = await _calculoDescontoService.CalcularValeTransporte(_funcionario.PossuiValeTransporte, totalRemuneracoes);
            return await AdicionarLancamentoAsync(DescricaoLancamentoEnum.ValeTransporte.ToString(), TipoLancamentoEnum.Desconto.ToString(), valor, _mesReferencia);
        }

        public async Task<ContrachequeBuilder> AdicionarDescontoFGTSAsync()
        {
            decimal totalRemuneracoes = CalcularTotalRemuneracoes();
            decimal valor = await _calculoDescontoService.CalcularFGTS(totalRemuneracoes);
            return await AdicionarLancamentoAsync(DescricaoLancamentoEnum.FGTS.ToString(), TipoLancamentoEnum.Desconto.ToString(), valor, _mesReferencia);
        }

        public async Task<ContrachequeBuilder> AdicionarRemuneracoesMesAsync()
        {
            var lancamentos = await _lancamentoRepository.GetByFuncionarioAndMesAsync(_funcionario.Id, _mesReferencia);
            foreach (var lancamento in lancamentos)
            {
                _lancamentos.Add(new LancamentoContracheque(
                    lancamento.Descricao,
                    TipoLancamentoEnum.Remuneracao.ToString(),
                    lancamento.Valor,
                    lancamento.DataLancamento));
            }

            _totalRemuneracoes = _lancamentos
                .Where(l => l.TipoLancamento == TipoLancamentoEnum.Remuneracao.ToString())
                .Sum(l => l.Valor);

            return await Task.FromResult(this);
        }

        public async Task<Contracheque> BuildAsync()
        {
            var totalDescontos = _lancamentos
                .Where(l => l.TipoLancamento == TipoLancamentoEnum.Desconto.ToString())
                .Sum(l => l.Valor);

            var totalRemuneracoes = CalcularTotalRemuneracoes();

            var salarioLiquido = totalRemuneracoes - totalDescontos;

            return await Task.FromResult(new Contracheque(
                _mesReferencia,
                _lancamentos,
                totalRemuneracoes,
                totalDescontos,
                salarioLiquido
            ));
        }
    }
}
