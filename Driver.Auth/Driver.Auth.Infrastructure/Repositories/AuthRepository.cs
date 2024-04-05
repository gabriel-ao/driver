using Dapper;
using Driver.Auth.Domain.Interfaces.Repositories;
using Driver.Auth.Domain.Models.Input;
using Driver.Auth.Domain.Models.Output;
using Newtonsoft.Json;

namespace Driver.Auth.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDapperConnectionFactory _connection;
        private readonly ILogRepository _logRepository;

        public AuthRepository(IDapperConnectionFactory connection, ILogRepository logRepository)
        {
            _connection = connection;
            _logRepository = logRepository;
        }

        public AuthOutputModal Auth(string userDate, string password)
        {
            var response = new AuthOutputModal();

            var QUERY = $"SELECT * FROM \"public\".\"AUT_Login\"(" +
                $"'{userDate}', " +
                $"'{password}')";

            try
            {
                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<AuthOutputModal>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "",
                    Message = JsonConvert.SerializeObject(ex.Message).Replace("'", "´"),
                    StackMessage = JsonConvert.SerializeObject(ex.Message).Replace("'", "´"),
                    Type = "Error"
                });

                response.Error = true;
                response.Message = ex.Message;
                return response;
            }
            finally
            {
                _connection.CloseConnection();
            }
        }
    }
}
