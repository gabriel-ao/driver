using Dapper;
using Driver.Domain.Interfaces.Repositories;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;
using Newtonsoft.Json;

namespace Driver.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IDapperConnectionFactory _connection;
        private readonly ILogRepository _logRepository;

        public DriverRepository(IDapperConnectionFactory connection, ILogRepository logRepository)
        {
            _connection = connection;
            _logRepository = logRepository;
        }

        public CreateDriverOutputModel CreateDriver(CreateDriverInput input)
        {
            var response = new CreateDriverOutputModel();

            var QUERY = $"SELECT * FROM \"public\".\"DRV_Create_Driver\"(" +
                $"'{input.FirstName}', " +
                $"'{input.LastName}', " +
                $"'{input.Cnpj}', " +
                $"'{input.BirthDate.ToString("yyyy-MM-dd")}', " +
                $"'{input.CnhNumber}', " +
                $"'{input.CnhID}', " +
                $"'{input.CnhImage}', " +
                $"'{input.Password}')";

            try
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Driver/Create",
                    Message = "verificando input de CreateDriver",
                    StackMessage = JsonConvert.SerializeObject(input),
                    Type = "Info"
                });

                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<CreateDriverOutputModel>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Driver/Create",
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
