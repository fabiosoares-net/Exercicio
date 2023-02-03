using Questao5.Domain.Entities;
using Questao5.Domain.Interface;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Domain.Service
{
    public class IdempotenciaService : IIdempotenciaService
    {
        private readonly IIdempotenciaRepository idempotenciaRepository;
        public IdempotenciaService(IIdempotenciaRepository idempotenciaRepository)
        {
            this.idempotenciaRepository = idempotenciaRepository;
        }

        public async Task<Idempotencia> Create(Idempotencia idempotencia)
        {
            return await idempotenciaRepository.Create(idempotencia);
        }

        public async Task<Idempotencia> Get(string id)
        {
            return await idempotenciaRepository.Get(id);
        }
    }
}
