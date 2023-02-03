using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using System.Linq;
using Questao5.Application.Queries.Responses;

namespace Questao5.Infrastructure.Database.Repository
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IDBContext dbContext;
        public ContaCorrenteRepository(IDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ContaCorrente> Get(string id)
        {
            var sql = @"select 
	                        idcontacorrente,
	                        numero,
	                        nome,
	                        ativo 
                        from contacorrente 
                        where idcontacorrente = @id";

            using (var connection = dbContext.ConnectionFactory())
            {
                return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { id = id.ToUpper() });
            }
        }
    }
}
