using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentoController : ControllerBase
    {
        private readonly IMediator mediator;

        public MovimentoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Post(CreateMovimentoCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
