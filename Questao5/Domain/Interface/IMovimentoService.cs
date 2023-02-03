using Questao5.Domain.Entities;

namespace Questao5.Domain.Interface
{
    public interface IMovimentoService
    {
        Task<Movimento> Create(Movimento movimento);
        Task<IEnumerable<SaldoContaCorrenteList>> List(string idContaCorrente);
    }
}
