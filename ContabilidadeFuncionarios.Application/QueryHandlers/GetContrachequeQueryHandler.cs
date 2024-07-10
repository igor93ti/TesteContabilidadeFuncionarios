using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ContabilidadeFuncionarios.Application.Queries;
using ContabilidadeFuncionarios.Domain;
using ContabilidadeFuncionarios.Domain.Interfaces;

namespace ContabilidadeFuncionarios.Application.QueryHandlers
{
    public class GetContrachequeQueryHandler : IRequestHandler<GetContrachequeQuery, Contracheque>
    {
        private readonly IContrachequeService _contrachequeService;

        public GetContrachequeQueryHandler(IContrachequeService contrachequeService)
        {
            _contrachequeService = contrachequeService;
        }

        public async Task<Contracheque> Handle(GetContrachequeQuery request, CancellationToken cancellationToken)
        {
            return await _contrachequeService.GerarContrachequeAsync(request.FuncionarioId, request.AnoMesReferencia);
        }
    }
}
