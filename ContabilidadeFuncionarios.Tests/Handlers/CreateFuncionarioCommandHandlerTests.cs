namespace ContabilidadeFuncionarios.Tests.Commands
{
    public class CreateFuncionarioCommandHandlerTests
    {
        private readonly Mock<IFuncionarioRepository> _funcionarioRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateFuncionarioCommandHandler _handler;

        public CreateFuncionarioCommandHandlerTests()
        {
            _funcionarioRepositoryMock = new Mock<IFuncionarioRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateFuncionarioCommandHandler(_funcionarioRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_DeveCriarNovoFuncionarioComSucesso()
        {
            // Arrange
            var command = new CreateFuncionarioCommand(
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

            var funcionario = new Funcionario(
                command.Nome,
                command.Sobrenome,
                command.Documento,
                command.Setor,
                command.SalarioBruto,
                command.DataAdmissao,
                command.PossuiPlanoSaude,
                command.PossuiPlanoDental,
                command.PossuiValeTransporte
            );

            _funcionarioRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Funcionario>()))
                .Returns(Task.FromResult(funcionario));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _funcionarioRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Funcionario>()), Times.Once);
        }
    }
}
