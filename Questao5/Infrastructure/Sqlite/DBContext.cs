using Microsoft.AspNetCore.Connections;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Sqlite
{
    public class DBContext : IDBContext
    {
        private readonly DatabaseConfig databaseConfig;

        public DBContext(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public IDbConnection ConnectionFactory()
        {
            try
            {
                var conn = new SqliteConnection(databaseConfig.Name);

                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
