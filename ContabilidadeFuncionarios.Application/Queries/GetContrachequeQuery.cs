using MediatR;
using ContabilidadeFuncionarios.Domain;

namespace ContabilidadeFuncionarios.Application.Queries
{
    public record GetContrachequeQuery(int FuncionarioId, DateTime AnoMesReferencia) : IRequest<Contracheque>;
}
