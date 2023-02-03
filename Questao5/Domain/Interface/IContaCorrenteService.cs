using Questao5.Domain.Entities;

namespace Questao5.Domain.Interface
{
    public interface IContaCorrenteService
    {
        Task<ContaCorrente> Get(string id);
        Task ContaCorrenteEhValida(string idContaCorrente);
    }
}
