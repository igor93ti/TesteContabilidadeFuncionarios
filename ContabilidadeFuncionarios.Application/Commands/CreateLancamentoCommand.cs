using MediatR;
using ContabilidadeFuncionarios.Domain.Enums;
using System;

namespace ContabilidadeFuncionarios.Application.Commands
{
    public record CreateLancamentoCommand(int FuncionarioId, DateTime DataLancamento, decimal Valor, string Descricao) : IRequest<int>;
}
