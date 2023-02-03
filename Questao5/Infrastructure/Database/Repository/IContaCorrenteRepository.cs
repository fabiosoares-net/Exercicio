using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repository
{
    public interface IContaCorrenteRepository : IRepository
    {
        Task<ContaCorrente> Get(string id);
    }
}
