using System.Data;

namespace Questao5.Infrastructure.Sqlite
{
    public interface IDBContext
    {
        IDbConnection ConnectionFactory();
    }
}
