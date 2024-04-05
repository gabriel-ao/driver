using Dapper;
using Driver.Auth.Domain.Interfaces.Repositories;
using Driver.Auth.Domain.Models.Base;
using Driver.Auth.Domain.Models.Input;
using Driver.Auth.Domain.Models.Output;

namespace Driver.Auth.Infrastructure.Services
{
    public class LogRepository : ILogRepository
    {
        private readonly IDapperConnectionFactory _connection;

        public LogRepository(IDapperConnectionFactory connection)
        {
            _connection = connection;
        }

        public BaseOutput CreateLog(CreateLogInput input)
        {
            var response = new BaseOutput();

            var QUERY = $"SELECT * FROM \"public\".\"SIS_Create_Logs\"(" +
                $"'{input.MethodName}', " +
                $"'{input.Message}', " +
                $"'{input.StackMessage}', " +
                $"'{input.Type}')";

            try
            {
                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<BaseOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
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
