using Dapper;
using Driver.Manager.Domain.Interfaces.Repositories;
using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Manager.Infrastructure.Repositories
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
