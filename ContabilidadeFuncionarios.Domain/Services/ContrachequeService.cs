using System;
using System.Threading.Tasks;
using ContabilidadeFuncionarios.Domain;
using ContabilidadeFuncionarios.Domain.Builders;
using ContabilidadeFuncionarios.Domain.Interfaces;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;

namespace ContabilidadeFuncionarios.Domain.Services
{
    public class ContrachequeService : IContrachequeService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly ICalculoDescontoService _calculoDescontoService;
        private readonly ILancamentoRepository _lancamentoRepository;

        public ContrachequeService(
            IFuncionarioRepository funcionarioRepository,
            ICalculoDescontoService calculoDescontoService,
            ILancamentoRepository lancamentoRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _calculoDescontoService = calculoDescontoService;
            _lancamentoRepository = lancamentoRepository;
        }

        public async Task<Contracheque> GerarContrachequeAsync(int funcionarioId, DateTime mesReferencia)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(funcionarioId);

            if (funcionario == null)
            {
                throw new ArgumentException("Funcionário não encontrado.");
            }

            var builder = new ContrachequeBuilder(funcionario, mesReferencia, _calculoDescontoService, _lancamentoRepository);

            await builder.AdicionarRemuneracoesMesAsync();
            await builder.AdicionarDescontoINSSAsync();
            await builder.AdicionarDescontoIRRFAsync();
            await builder.AdicionarDescontoPlanoSaudeAsync();
            await builder.AdicionarDescontoPlanoDentalAsync();
            await builder.AdicionarDescontoValeTransporteAsync();
            await builder.AdicionarDescontoFGTSAsync();

            return await builder.BuildAsync();
        }
    }
}
