using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Interfaces;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;
using MediatR;

namespace ContabilidadeFuncionarios.Application.Commands.CreateFuncionario
{
    public class CreateFuncionarioCommandHandler(IFuncionarioRepository FuncionarioRepository, IUnitOfWork UnitOfWork) : IRequestHandler<CreateFuncionarioCommand, int>
    {
        private readonly IFuncionarioRepository _funcionarioRepository = FuncionarioRepository;
        private readonly IUnitOfWork _unitOfWork = UnitOfWork;

        public async Task<int> Handle(CreateFuncionarioCommand request, CancellationToken cancellationToken)
        {
            var funcionario = new Funcionario(
                request.Nome,
                request.Sobrenome,
                request.Documento,
                request.Setor,
                request.SalarioBruto,
                request.DataAdmissao,
                request.PossuiPlanoSaude,
                request.PossuiPlanoDental,
                request.PossuiValeTransporte
            );

            await _funcionarioRepository.AddAsync(funcionario);
            await _unitOfWork.CommitAsync();

            return funcionario.Id;
        }
    }
}
