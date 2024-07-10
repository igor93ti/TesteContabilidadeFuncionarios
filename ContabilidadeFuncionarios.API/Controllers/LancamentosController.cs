using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ContabilidadeFuncionarios.Application.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace ContabilidadeFuncionarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LancamentosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adiciona as remunerações de um funcionário para um determinado mês")]
        public async Task<IActionResult> Create([FromBody] CreateLancamentoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
