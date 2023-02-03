using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaldoContaCorrenteController : ControllerBase
    {
        private readonly IMediator mediator;

        public SaldoContaCorrenteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("obtersaldo")]
        public async Task<IActionResult> Get(GetSaldoContaCorrenteByIdQuery query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
