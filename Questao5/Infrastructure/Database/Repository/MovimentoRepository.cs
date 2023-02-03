using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Sqlite;
using System.Drawing;
using System.Reflection;

namespace Questao5.Infrastructure.Database.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly IDBContext dbContext;
        public MovimentoRepository(IDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Movimento> Create(Movimento movimento)
        {
            var sql = @"INSERT INTO movimento
	                        (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                        VALUES
	                        (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";

            using (var connection = dbContext.ConnectionFactory())
            {
                await connection.ExecuteAsync(sql, new
                {
                    IdMovimento = movimento.IdMovimento,
                    IdContaCorrente = movimento.IdContaCorrente,
                    DataMovimento = movimento.DataMovimento,
                    TipoMovimento = movimento.TipoMovimento,
                    Valor = movimento.Valor
                });
            }

            return movimento;
        }

        public async Task<IEnumerable<SaldoContaCorrenteList>> List(string id)
        {
            var sql = @"select 
	                        c.numero, 
	                        c.nome, 
	                        m.valor,
	                        m.tipomovimento	
                        from contacorrente c
                        join movimento m on m.idcontacorrente = c.idcontacorrente
                        where c.idcontacorrente = @id";

            using (var connection = dbContext.ConnectionFactory())
            {
                return await connection.QueryAsync<SaldoContaCorrenteList>(sql, new { id = id.ToUpper() });
            }
        }

        public async Task<IEnumerable<Movimento>> List()
        {
            var sql = @"select 
	                        idmovimento,
                            idcontacorrente,
                            datamovimento,
                            tipomovimento,
                            valor
                        from movimento";

            using (var connection = dbContext.ConnectionFactory())
            {
                return await connection.QueryAsync<Movimento>(sql);
            }
        }
    }
}
