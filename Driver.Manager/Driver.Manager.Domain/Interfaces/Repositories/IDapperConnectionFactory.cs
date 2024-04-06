using System.Data;

namespace Driver.Manager.Domain.Interfaces.Repositories
{
    public interface IDapperConnectionFactory
    {
        IDbConnection GetConnection { get; }
        void CloseConnection();
    }
}
