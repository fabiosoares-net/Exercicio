using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repository
{
    public interface IMovimentoRepository : IRepository
    {
        Task<Movimento> Create(Movimento movimento);
        Task<IEnumerable<SaldoContaCorrenteList>> List(string id);
        Task<IEnumerable<Movimento>> List();
    }
}
