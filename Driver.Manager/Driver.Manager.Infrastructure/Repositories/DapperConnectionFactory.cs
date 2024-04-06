using Driver.Manager.Domain.Interfaces.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Manager.Infrastructure.Repositories
{
    public class DapperConnectionFactory : IDapperConnectionFactory
    {
        private IDbConnection _connection;

        public DapperConnectionFactory()
        {

        }

        public IDbConnection GetConnection
        {
            get
            {
                try
                {
                    string connectionString = "Server=localhost;Database=driver-db;Port=5433;User Id=postgres;Password=postgres;";

                    _connection = new NpgsqlConnection(connectionString);

                    if (_connection.State != ConnectionState.Open)
                    {
                        _connection.Open();
                    }
                }
                catch (Exception ex) { }

                return _connection;
            }

        }

        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
