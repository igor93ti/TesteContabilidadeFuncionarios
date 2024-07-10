using MediatR;
using ContabilidadeFuncionarios.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using ContabilidadeFuncionarios.Application.Queries.GetFuncionarioById;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;

namespace ContabilidadeFuncionarios.Application.QueryHandlers
{
    public class GetFuncionarioByIdQueryHandler : IRequestHandler<GetFuncionarioByIdQuery, Funcionario>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public GetFuncionarioByIdQueryHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<Funcionario> Handle(GetFuncionarioByIdQuery request, CancellationToken cancellationToken)
        {
            return await _funcionarioRepository.GetByIdAsync(request.Id);
        }
    }
}
