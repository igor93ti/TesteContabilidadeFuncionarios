using Microsoft.AspNetCore.Mvc;
using ContabilidadeFuncionarios.Application.Commands.CreateFuncionario;
using MediatR;
using System.Threading.Tasks;
using ContabilidadeFuncionarios.Application.Queries.GetFuncionarioById;
using ContabilidadeFuncionarios.Application.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace ContabilidadeFuncionarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FuncionariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Cria um novo funcionário")]
        public async Task<IActionResult> CreateFuncionario([FromBody] CreateFuncionarioCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetFuncionarioById), new { id = result }, result);
        }

        [SwaggerOperation(Summary = "Obtém um funcionário pelo ID")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FuncionarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFuncionarioById(int id)
        {
            var funcionario = await _mediator.Send(new GetFuncionarioByIdQuery { Id = id });
            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }
    }
}
