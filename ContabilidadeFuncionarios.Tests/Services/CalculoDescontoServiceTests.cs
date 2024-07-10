namespace ContabilidadeFuncionarios.Tests.Services
{
    public class CalculoDescontoServiceTests
    {
        private readonly Mock<ITaxaDescontoRepository> _taxaDescontoRepositoryMock;
        private readonly CalculoDescontoService _calculoDescontoService;

        public CalculoDescontoServiceTests()
        {
            _taxaDescontoRepositoryMock = new Mock<ITaxaDescontoRepository>();
            _calculoDescontoService = new CalculoDescontoService(_taxaDescontoRepositoryMock.Object);
        }

        [Theory]
        [InlineData(1000, 1000 * 0.075)]
        [InlineData(1500, 1500 * 0.09)]
        [InlineData(2500, 2500 * 0.12)]
        [InlineData(5000, 5000 * 0.14)]
        [InlineData(15000, 6101.06 * 0.14)]
        public async Task CalcularINSS_DeveRetornarValorCorreto(decimal salarioBruto, decimal valorEsperado)
        {
            // Arrange
            var faixasINSS = new List<TaxaDesconto>
            {
                new TaxaDesconto(DescricaoLancamentoEnum.INSS, 0m, 1045m, 0.075m, 0m),
                new TaxaDesconto(DescricaoLancamentoEnum.INSS, 1045.01m, 2089.60m, 0.09m, 0m),
                new TaxaDesconto(DescricaoLancamentoEnum.INSS, 2089.61m, 3134.40m, 0.12m, 0m),
                new TaxaDesconto(DescricaoLancamentoEnum.INSS, 3134.41m, 6101.06m, 0.14m, 0m)
            };
            _taxaDescontoRepositoryMock.Setup(repo => repo.GetByTipoAsync(DescricaoLancamentoEnum.INSS))
                .ReturnsAsync(faixasINSS);

            // Act
            var result = await _calculoDescontoService.CalcularINSS(salarioBruto);

            // Assert
            Assert.Equal(valorEsperado, result);
        }

        [Theory]
        [InlineData(2000, 150, 142.8)]
        [InlineData(3000, 450, 354.8)]
        [InlineData(4000, 900, 636.13)]
        [InlineData(5000, 1375, 869.36)]
        public async Task CalcularIRRF_DeveRetornarValorCorreto(decimal salarioBruto, decimal IRRFEsperado, decimal DeducaoEsperada)
        {
            // Arrange
            var faixasIRRF = new List<TaxaDesconto>
            {
                new TaxaDesconto(DescricaoLancamentoEnum.IRRF, 0m, 1903.98m, 0m, 0m),
                new TaxaDesconto(DescricaoLancamentoEnum.IRRF, 1903.99m, 2826.65m, 0.075m, 142.8m),
                new TaxaDesconto(DescricaoLancamentoEnum.IRRF, 2826.66m, 3751.05m, 0.15m, 354.8m),
                new TaxaDesconto(DescricaoLancamentoEnum.IRRF, 3751.06m, 4664.68m, 0.225m, 636.13m),
                new TaxaDesconto(DescricaoLancamentoEnum.IRRF, 4664.69m, decimal.MaxValue, 0.275m, 869.36m)
            };
            _taxaDescontoRepositoryMock.Setup(repo => repo.GetByTipoAsync(DescricaoLancamentoEnum.IRRF))
                .ReturnsAsync(faixasIRRF);

            // Act
            var result = await _calculoDescontoService.CalcularIRRF(salarioBruto);

            // Assert
            Assert.Equal(Math.Min(IRRFEsperado, DeducaoEsperada), result);
        }

        [Theory]
        [InlineData(true, 10)]
        [InlineData(false, 0)]
        public async Task CalcularPlanoSaude_DeveRetornarValorCorreto(bool possuiPlanoSaude, decimal valorEsperado)
        {
            // Arrange
            var planoSaude = new List<TaxaDesconto>
            {
                new TaxaDesconto(DescricaoLancamentoEnum.PlanoSaude, 0m, decimal.MaxValue, 10m, 0m)
            };
            _taxaDescontoRepositoryMock.Setup(repo => repo.GetByTipoAsync(DescricaoLancamentoEnum.PlanoSaude))
                .ReturnsAsync(planoSaude);

            // Act
            var result = await _calculoDescontoService.CalcularPlanoSaude(possuiPlanoSaude);

            // Assert
            Assert.Equal(valorEsperado, result);
        }

        [Theory]
        [InlineData(true, 5)]
        [InlineData(false, 0)]
        public async Task CalcularPlanoDental_DeveRetornarValorCorreto(bool possuiPlanoDental, decimal valorEsperado)
        {
            // Arrange
            var planoDental = new List<TaxaDesconto>
            {
                new TaxaDesconto(DescricaoLancamentoEnum.PlanoDental, 0m, decimal.MaxValue, 5m, 0m)
            };
            _taxaDescontoRepositoryMock.Setup(repo => repo.GetByTipoAsync(DescricaoLancamentoEnum.PlanoDental))
                .ReturnsAsync(planoDental);

            // Act
            var result = await _calculoDescontoService.CalcularPlanoDental(possuiPlanoDental);

            // Assert
            Assert.Equal(valorEsperado, result);
        }

        [Theory]
        [InlineData(true, 2000, 120)]
        [InlineData(true, 1000, 60)]
        [InlineData(false, 2000, 0)]
        public async Task CalcularValeTransporte_DeveRetornarValorCorreto(bool possuiValeTransporte, decimal salarioBruto, decimal valorEsperado)
        {
            // Arrange
            var valeTransporte = new List<TaxaDesconto>
            {
                new TaxaDesconto(DescricaoLancamentoEnum.ValeTransporte, 0m, decimal.MaxValue, 0.06m, 0m)
            };
            _taxaDescontoRepositoryMock.Setup(repo => repo.GetByTipoAsync(DescricaoLancamentoEnum.ValeTransporte))
                .ReturnsAsync(valeTransporte);

            // Act
            var result = await _calculoDescontoService.CalcularValeTransporte(possuiValeTransporte, salarioBruto);

            // Assert
            Assert.Equal(valorEsperado, result);
        }

        [Theory]
        [InlineData(2000, 160)]
        [InlineData(5000, 400)]
        public async Task CalcularFGTS_DeveRetornarValorCorreto(decimal salarioBruto, decimal valorEsperado)
        {
            // Arrange
            var fgts = new List<TaxaDesconto>
            {
                new TaxaDesconto(DescricaoLancamentoEnum.FGTS, 0m, decimal.MaxValue, 0.08m, 0m)
            };
            _taxaDescontoRepositoryMock.Setup(repo => repo.GetByTipoAsync(DescricaoLancamentoEnum.FGTS))
                .ReturnsAsync(fgts);

            // Act
            var result = await _calculoDescontoService.CalcularFGTS(salarioBruto);

            // Assert
            Assert.Equal(valorEsperado, result);
        }
    }
}
