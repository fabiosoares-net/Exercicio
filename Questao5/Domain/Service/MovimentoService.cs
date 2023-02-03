using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Helper;
using Questao5.Domain.Interface;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Domain.Service
{
    public class MovimentoService : IMovimentoService
    {
        private readonly IContaCorrenteService contaCorrenteService;
        private readonly IMovimentoRepository movimentoRepository;

        public MovimentoService(
            IContaCorrenteService contaCorrenteService,
            IMovimentoRepository movimentoRepository)
        {
            this.contaCorrenteService = contaCorrenteService;
            this.movimentoRepository = movimentoRepository;
        }

        public async Task<Movimento> Create(Movimento movimento)
        {
            Validar(movimento);

            return await movimentoRepository.Create(movimento);
        }

        public async Task<IEnumerable<SaldoContaCorrenteList>> List(string idContaCorrente)
        {
            return await movimentoRepository.List(idContaCorrente);
        }

        private void Validar(Movimento movimento)
        {
            if (movimento.Valor < 0 || movimento.Valor == 0)
            {
                throw new BusinessException(new ResponseError() { Mensagem = "Apenas valores positivos podem ser recebidos", TipoFalha = "INVALID_VALUE" });
            }

            if (movimento.TipoMovimento != (char)Domain.Enumerators.TipoMovimento.CREDITO && movimento.TipoMovimento != (char)Domain.Enumerators.TipoMovimento.DEBITO)
            {
                throw new BusinessException(new ResponseError() { Mensagem = "Apenas os tipos “débito” ou “crédito” podem ser aceitos", TipoFalha = "INVALID_TYPE" });
            }
        }
    }
}
