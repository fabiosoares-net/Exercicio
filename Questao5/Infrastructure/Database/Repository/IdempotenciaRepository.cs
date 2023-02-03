using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repository
{
    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        private readonly IDBContext dbContext;
        public IdempotenciaRepository(IDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Idempotencia> Create(Idempotencia idempotencia)
        {
            var sql = @"INSERT INTO idempotencia
	                        (chave_idempotencia, requisicao, resultado) 
                        VALUES
	                        (@chave_idempotencia, @requisicao, @resultado)";

            using (var connection = dbContext.ConnectionFactory())
            {
                await connection.ExecuteAsync(sql, new
                {
                    chave_idempotencia = idempotencia.ChaveIdempotencia,
                    requisicao = idempotencia.Requisicao,
                    resultado = idempotencia.Resultado
                });
            }

            return idempotencia;
        }

        public async Task<Idempotencia> Get(string id)
        {
            var sql = @"select 
                            chave_idempotencia, 
                            requisicao, 
                            resultado
                        from idempotencia 
                        where chave_idempotencia = @id";

            using (var connection = dbContext.ConnectionFactory())
            {
                return await connection.QueryFirstOrDefaultAsync<Idempotencia>(sql, new { id = id.ToUpper() });
            }
        }
    }
}
