using System.Data;

namespace Driver.Domain.Interfaces.Repositories
{
    public interface IDapperConnectionFactory
    {
        IDbConnection GetConnection { get; }
        void CloseConnection();
    }
}
