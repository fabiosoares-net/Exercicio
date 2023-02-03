using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repository
{
    public interface IIdempotenciaRepository : IRepository
    {
        Task<Idempotencia> Create(Idempotencia idempotencia);
        Task<Idempotencia> Get(string id);
    }
}
