using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Helper;
using Questao5.Domain.Interface;
using Questao5.Domain.Service;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Application.Handlers
{
    public class CreateMovimentoHandler : IRequestHandler<CreateMovimentoCommand, CreateMovimentoResponse>
    {
        private readonly IContaCorrenteService contaCorrenteService;
        private readonly IMovimentoService movimentoService;
        private readonly IIdempotenciaService idempotenciaService;

        public CreateMovimentoHandler(
            IContaCorrenteService contaCorrenteService,
            IMovimentoService movimentoService,
            IIdempotenciaService idempotenciaService)
        {
            this.contaCorrenteService = contaCorrenteService;
            this.movimentoService = movimentoService;
            this.idempotenciaService = idempotenciaService;
        }

        public async Task<CreateMovimentoResponse> Handle(CreateMovimentoCommand request, CancellationToken cancellationToken)
        {
            Validar(request);

            var existeRequisicao = await idempotenciaService.Get(request.IdRequisicao);

            if (existeRequisicao != null)
            {
                return new CreateMovimentoResponse() { IdMovimento = existeRequisicao.Resultado };
            }
            else
            {
                await contaCorrenteService.ContaCorrenteEhValida(request.IdContaCorrente);

                var movimento = new Movimento()
                {
                    IdMovimento = Guid.NewGuid().ToString().ToUpper(),
                    IdContaCorrente = request.IdContaCorrente.ToUpper(),
                    DataMovimento = DateTime.Now,
                    TipoMovimento = Char.ToUpper(request.TipoMovimento),
                    Valor = request.Valor
                };

                await movimentoService.Create(movimento);

                await idempotenciaService.Create(new Idempotencia()
                {
                    ChaveIdempotencia = request.IdRequisicao.ToUpper(),
                    Requisicao = "CreateMovimentoCommand",
                    Resultado = movimento.IdMovimento
                });

                return new CreateMovimentoResponse() { IdMovimento = movimento.IdMovimento };
            }
        }

        private void Validar(CreateMovimentoCommand request)
        {
            if (request == null)
            {
                throw new BusinessException("O Movimento é obrigatório");
            }

            if (string.IsNullOrEmpty(request.IdRequisicao))
            {
                throw new BusinessException("O IdRequisicao é obrigatório");
            }

            if (string.IsNullOrEmpty(request.IdContaCorrente))
            {
                throw new BusinessException("O IdContaCorrente é obrigatório");
            }

            if (request.Valor == 0)
            {
                throw new BusinessException("O Valor é obrigatório");
            }

            if (string.IsNullOrEmpty(request.TipoMovimento.ToString()))
            {
                throw new BusinessException("O Tipo do Movimento é obrigatório");
            }
        }
    }
}
