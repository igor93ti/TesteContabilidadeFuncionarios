using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Interfaces;
using ContabilidadeFuncionarios.Application.Commands;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;

namespace ContabilidadeFuncionarios.Application.Handlers
{
    public class CreateLancamentoCommandHandler : IRequestHandler<CreateLancamentoCommand, int>
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLancamentoCommandHandler(ILancamentoRepository lancamentoRepository, IUnitOfWork unitOfWork)
        {
            _lancamentoRepository = lancamentoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateLancamentoCommand request, CancellationToken cancellationToken)
        {
            var lancamento = new Lancamento(request.FuncionarioId, request.DataLancamento, request.Valor, request.Descricao);

            await _lancamentoRepository.AddAsync(lancamento);
            await _unitOfWork.CommitAsync();

            return lancamento.Id;
        }
    }
}
