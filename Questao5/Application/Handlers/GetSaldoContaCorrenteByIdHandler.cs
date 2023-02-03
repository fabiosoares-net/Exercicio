using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Helper;
using Questao5.Domain.Interface;
using Questao5.Domain.Service;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Application.Handlers
{
    public class GetSaldoContaCorrenteByIdHandler : IRequestHandler<GetSaldoContaCorrenteByIdQuery, GetSaldoContaCorrenteByIdResponse>
    {
        private readonly IContaCorrenteService contaCorrenteService;
        private readonly IMovimentoService movimentoService;

        public GetSaldoContaCorrenteByIdHandler(IMovimentoService movimentoService, IContaCorrenteService contaCorrenteService)
        {
            this.movimentoService = movimentoService;
            this.contaCorrenteService = contaCorrenteService;
        }

        public async Task<GetSaldoContaCorrenteByIdResponse> Handle(GetSaldoContaCorrenteByIdQuery request, CancellationToken cancellationToken)
        {
            Validar(request);

            await contaCorrenteService.ContaCorrenteEhValida(request.IdContaCorrente);

            var listContaCorrente = await movimentoService.List(request.IdContaCorrente);

            var somaCredito = listContaCorrente
                .Where(x => x.TipoMovimento == (char)Domain.Enumerators.TipoMovimento.CREDITO).ToList()
                .Sum(x => x.Valor);

            var somaDebito = listContaCorrente
                .Where(x => x.TipoMovimento == (char)Domain.Enumerators.TipoMovimento.DEBITO).ToList()
                .Sum(x => x.Valor);

            var saldo = (somaCredito - somaDebito);

            var getSaldoContaCorrenteByIdResponse = new GetSaldoContaCorrenteByIdResponse()
            {
                Numero = listContaCorrente.First().Numero,
                Nome = listContaCorrente.First().Nome,
                DataHoraResposta = DateTime.Now.ToString("G"),
                Saldo = saldo
            };

            return getSaldoContaCorrenteByIdResponse;
        }

        private void Validar(GetSaldoContaCorrenteByIdQuery request)
        {
            if (request == null)
            {
                throw new BusinessException("O Movimento é obrigatório");
            }

            if (string.IsNullOrEmpty(request.IdContaCorrente))
            {
                throw new BusinessException("O IdContaCorrente é obrigatório");
            }
        }
    }
}
