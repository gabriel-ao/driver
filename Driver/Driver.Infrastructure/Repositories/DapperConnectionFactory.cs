using Driver.Domain.Interfaces.Repositories;
using Npgsql;
using System.Data;

namespace Driver.Infrastructure.Repositories
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
                    //string connString = "Host=localhost;Port=5432;Username=seu_usuario;Password=sua_senha;Database=seu_banco_de_dados";

                    //string connectionString = "Host=localhost;Port=5432;Database=driver-db;User Id=postgres;Password=gabriel;"; // internet
                    //string connectionString = "Server=127.0.0.1;Database=driver-db;Port=5433;User Id=postgres;Password=postgres;";
                    string connectionString = "Server=localhost;Database=driver-db;Port=5433;User Id=postgres;Password=postgres;";


                    _connection = new NpgsqlConnection(connectionString);


                    //_connection = new NpgsqlConnection("Server=localhost;Database=Vagga-DB;Port=5432;User Id=postgres;Password=gabriel;");

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
