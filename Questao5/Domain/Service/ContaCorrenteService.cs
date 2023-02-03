using Questao5.Domain.Entities;
using Questao5.Domain.Helper;
using Questao5.Domain.Interface;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Domain.Service
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly IContaCorrenteRepository contaCorrenteRepository;
        public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
        {
            this.contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task ContaCorrenteEhValida(string idContaCorrente)
        {
            var contaCorrente = await contaCorrenteRepository.Get(idContaCorrente);

            if (contaCorrente == null)
            {
                throw new BusinessException(new ResponseError() { Mensagem = "Apenas contas correntes cadastradas podem consultar o saldo", TipoFalha = "INVALID_ACCOUNT" });
            }

            if (contaCorrente.Ativo == false)
            {
                throw new BusinessException(new ResponseError() { Mensagem = "Apenas contas correntes ativas podem receber movimentação", TipoFalha = "INACTIVE_ACCOUNT" });
            }
        }

        public async Task<ContaCorrente> Get(string id)
        {
            return await contaCorrenteRepository.Get(id);
        }
    }
}
