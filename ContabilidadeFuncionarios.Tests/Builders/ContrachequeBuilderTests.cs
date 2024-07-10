

namespace ContabilidadeFuncionarios.Tests.Builders
{
    public class ContrachequeBuilderTests
    {
        private readonly Mock<ICalculoDescontoService> _calculoDescontoServiceMock;
        private readonly Mock<ILancamentoRepository> _lancamentoRepositoryMock;
        private readonly Funcionario _funcionario;
        private readonly DateTime _mesReferencia;

        public ContrachequeBuilderTests()
        {
            _calculoDescontoServiceMock = new Mock<ICalculoDescontoService>();
            _lancamentoRepositoryMock = new Mock<ILancamentoRepository>();
            _funcionario = new Funcionario(
                "João",
                "Silva",
                "12345678901",
                "TI",
                5000m,
                new DateTime(2020, 1, 1),
                true,
                true,
                true
                );

            _mesReferencia = new DateTime(2024, 6, 1);
        }

        [Fact]
        public async Task AdicionarLancamentoAsync_DeveAdicionarLancamentoDeRemuneracao()
        {
            // Arrange
            var builder = new ContrachequeBuilder(_funcionario, _mesReferencia, _calculoDescontoServiceMock.Object, _lancamentoRepositoryMock.Object);

            // Act
            await builder.AdicionarLancamentoAsync("Salário Base", TipoLancamentoEnum.Remuneracao.ToString(), 5000m, _mesReferencia);

            // Assert
            var contracheque = await builder.BuildAsync();
            Assert.Single(contracheque.Lancamentos);
            Assert.Equal(5000m, contracheque.SalarioBruto);
            Assert.Equal("Salário Base", contracheque.Lancamentos.First().DescricaoLacamento);
        }

        [Fact]
        public async Task AdicionarDescontoINSSAsync_DeveCalcularEAdicionarDescontoINSS()
        {
            // Arrange
            _calculoDescontoServiceMock.Setup(s => s.CalcularINSS(It.IsAny<decimal>())).ReturnsAsync(700m);
            var builder = new ContrachequeBuilder(_funcionario, _mesReferencia, _calculoDescontoServiceMock.Object, _lancamentoRepositoryMock.Object);
            await builder.AdicionarLancamentoAsync("Salário Base", TipoLancamentoEnum.Remuneracao.ToString(), 5000m, _mesReferencia);

            // Act
            await builder.AdicionarDescontoINSSAsync();

            // Assert
            var contracheque = await builder.BuildAsync();
            var descontoINSS = contracheque.Lancamentos.FirstOrDefault(l => l.DescricaoLacamento == DescricaoLancamentoEnum.INSS.ToString());
            Assert.NotNull(descontoINSS);
            Assert.Equal(700m, descontoINSS.Valor);
            Assert.Equal(700m, contracheque.TotalDescontos);
        }

        [Fact]
        public async Task AdicionarDescontoIRRFAsync_DeveCalcularEAdicionarDescontoIRRF()
        {
            // Arrange
            _calculoDescontoServiceMock.Setup(s => s.CalcularIRRF(It.IsAny<decimal>())).ReturnsAsync(400m);
            var builder = new ContrachequeBuilder(_funcionario, _mesReferencia, _calculoDescontoServiceMock.Object, _lancamentoRepositoryMock.Object);
            await builder.AdicionarLancamentoAsync("Salário Base", TipoLancamentoEnum.Remuneracao.ToString(), 5000m, _mesReferencia);

            // Act
            await builder.AdicionarDescontoIRRFAsync();

            // Assert
            var contracheque = await builder.BuildAsync();
            var descontoIRRF = contracheque.Lancamentos.FirstOrDefault(l => l.DescricaoLacamento == DescricaoLancamentoEnum.IRRF.ToString());
            Assert.NotNull(descontoIRRF);
            Assert.Equal(400m, descontoIRRF.Valor);
            Assert.Equal(400m, contracheque.TotalDescontos);
        }

        [Fact]
        public async Task AdicionarDescontoPlanoSaudeAsync_DeveCalcularEAdicionarDescontoPlanoSaude()
        {
            // Arrange
            _calculoDescontoServiceMock.Setup(s => s.CalcularPlanoSaude(It.IsAny<bool>())).ReturnsAsync(10m);
            var builder = new ContrachequeBuilder(_funcionario, _mesReferencia, _calculoDescontoServiceMock.Object, _lancamentoRepositoryMock.Object);

            // Act
            await builder.AdicionarDescontoPlanoSaudeAsync();

            // Assert
            var contracheque = await builder.BuildAsync();
            var descontoPlanoSaude = contracheque.Lancamentos.FirstOrDefault(l => l.DescricaoLacamento == DescricaoLancamentoEnum.PlanoSaude.ToString());
            Assert.NotNull(descontoPlanoSaude);
            Assert.Equal(10m, descontoPlanoSaude.Valor);
            Assert.Equal(10m, contracheque.TotalDescontos);
        }

        [Fact]
        public async Task AdicionarDescontoPlanoDentalAsync_DeveCalcularEAdicionarDescontoPlanoDental()
        {
            // Arrange
            _calculoDescontoServiceMock.Setup(s => s.CalcularPlanoDental(It.IsAny<bool>())).ReturnsAsync(5m);
            var builder = new ContrachequeBuilder(_funcionario, _mesReferencia, _calculoDescontoServiceMock.Object, _lancamentoRepositoryMock.Object);

            // Act
            await builder.AdicionarDescontoPlanoDentalAsync();

            // Assert
            var contracheque = await builder.BuildAsync();
            var descontoPlanoDental = contracheque.Lancamentos.FirstOrDefault(l => l.DescricaoLacamento == DescricaoLancamentoEnum.PlanoDental.ToString());
            Assert.NotNull(descontoPlanoDental);
            Assert.Equal(5m, descontoPlanoDental.Valor);
            Assert.Equal(5m, contracheque.TotalDescontos);
        }

        [Fact]
        public async Task AdicionarDescontoValeTransporteAsync_DeveCalcularEAdicionarDescontoValeTransporte()
        {
            // Arrange
            _calculoDescontoServiceMock.Setup(s => s.CalcularValeTransporte(It.IsAny<bool>(), It.IsAny<decimal>())).ReturnsAsync(300m);
            var builder = new ContrachequeBuilder(_funcionario, _mesReferencia, _calculoDescontoServiceMock.Object, _lancamentoRepositoryMock.Object);
            await builder.AdicionarLancamentoAsync("Salário Base", TipoLancamentoEnum.Remuneracao.ToString(), 5000m, _mesReferencia);

            // Act
            await builder.AdicionarDescontoValeTransporteAsync();

            // Assert
            var contracheque = await builder.BuildAsync();
            var descontoValeTransporte = contracheque.Lancamentos.FirstOrDefault(l => l.DescricaoLacamento == DescricaoLancamentoEnum.ValeTransporte.ToString());
            Assert.NotNull(descontoValeTransporte);
            Assert.Equal(300m, descontoValeTransporte.Valor);
            Assert.Equal(300m, contracheque.TotalDescontos);
        }

        [Fact]
        public async Task AdicionarDescontoFGTSAsync_DeveCalcularEAdicionarDescontoFGTS()
        {
            // Arrange
            _calculoDescontoServiceMock.Setup(s => s.CalcularFGTS(It.IsAny<decimal>())).ReturnsAsync(400m);
            var builder = new ContrachequeBuilder(_funcionario, _mesReferencia, _calculoDescontoServiceMock.Object, _lancamentoRepositoryMock.Object);
            await builder.AdicionarLancamentoAsync("Salário Base", TipoLancamentoEnum.Remuneracao.ToString(), 5000m, _mesReferencia);

            // Act
            await builder.AdicionarDescontoFGTSAsync();

            // Assert
            var contracheque = await builder.BuildAsync();
            var descontoFGTS = contracheque.Lancamentos.FirstOrDefault(l => l.DescricaoLacamento == DescricaoLancamentoEnum.FGTS.ToString());
            Assert.NotNull(descontoFGTS);
            Assert.Equal(400m, descontoFGTS.Valor);
            Assert.Equal(400m, contracheque.TotalDescontos);
        }

        [Fact]
        public async Task AdicionarRendimentosMesAsync_DeveAdicionarRendimentosDoMes()
        {
            // Arrange
            var lancamentos = new List<Lancamento>
            {
                new Lancamento (funcionarioId:1, descricao: "Bônus", valor: 1000m, dataLancamento: _mesReferencia )
            };

            _lancamentoRepositoryMock.Setup(repo => repo.GetByFuncionarioAndMesAsync(_funcionario.Id, _mesReferencia))
                                     .ReturnsAsync(lancamentos);

            var builder = new ContrachequeBuilder(_funcionario, _mesReferencia, _calculoDescontoServiceMock.Object, _lancamentoRepositoryMock.Object);

            // Act
            await builder.AdicionarRemuneracoesMesAsync();

            // Assert
            var contracheque = await builder.BuildAsync();
            var bonus = contracheque.Lancamentos.FirstOrDefault(l => l.DescricaoLacamento == "Bônus");
            Assert.NotNull(bonus);
            Assert.Equal(1000m, bonus.Valor);
            Assert.Equal(1000m, contracheque.SalarioBruto);
        }
    }
}
