using MediatR;
using ContabilidadeFuncionarios.Domain.Entities;

namespace ContabilidadeFuncionarios.Application.Queries.GetFuncionarioById
{
    public class GetFuncionarioByIdQuery : IRequest<Funcionario>
    {
        public int Id { get; set; }
    }
}
