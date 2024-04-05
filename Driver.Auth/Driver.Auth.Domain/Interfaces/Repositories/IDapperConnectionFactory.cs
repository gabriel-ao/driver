using System.Data;

namespace Driver.Auth.Domain.Interfaces.Repositories
{
    public interface IDapperConnectionFactory
    {
        IDbConnection GetConnection { get; }
        void CloseConnection();
    }
}
