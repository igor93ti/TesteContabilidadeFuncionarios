using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ContabilidadeFuncionarios.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace ContabilidadeFuncionarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContrachequesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContrachequesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{funcionarioId}")]
        [SwaggerOperation(Summary = "Obtém o contracheque de um funcionário para um mês específico", Description = "Formato do parâmetro anoMesReferencia é 'aaaa-mm'")]
        public async Task<IActionResult> GetContracheque(
        int funcionarioId,[FromQuery, SwaggerParameter("Mês e ano de referência no formato 'aaaa-mm'", Required = true)] string anoMesReferencia)
        {
            var query = new GetContrachequeQuery(funcionarioId, DateTime.Parse(anoMesReferencia));
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
