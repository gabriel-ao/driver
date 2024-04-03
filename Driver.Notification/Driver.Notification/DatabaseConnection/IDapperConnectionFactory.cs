using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Notification.DatabaseConnection
{
    public interface IDapperConnectionFactory
    {
        IDbConnection GetConnection { get; }
        void CloseConnection();
    }
}
