using Questao5.Domain.Entities;

namespace Questao5.Domain.Interface
{
    public interface IIdempotenciaService
    {
        Task<Idempotencia> Create(Idempotencia idempotencia);
        Task<Idempotencia> Get(string id);
    }
}
