using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class GetSaldoContaCorrenteByIdQuery : IRequest<GetSaldoContaCorrenteByIdResponse>
    {
        public string IdContaCorrente { get; set; }
    }
}
