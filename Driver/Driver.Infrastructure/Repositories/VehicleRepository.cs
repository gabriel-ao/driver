using Dapper;
using Driver.Domain.Interfaces.Repositories;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Driver.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDapperConnectionFactory _connection;

        public VehicleRepository(IDapperConnectionFactory connection)
        {
            _connection = connection;
        }

        public BaseOutput CreateLog(CreateLogInput input)
        {
            var response = new BaseOutput();

            var QUERY = $"SELECT * FROM \"public\".\"Create_Logs\"(" +
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

        public BaseOutput UpdateVehicle(UpdateVehicleInputModel input)
        {
            var response = new BaseOutput();

            var QUERY = $"SELECT * FROM \"public\".\"ADM_Update_Vehicle\"(" +
                $"'{input.NewPlate}', " +
                $"'{input.VehicleId}', " +
                $"'{input.UserId}')";

            try
            {
                CreateLog(new CreateLogInput
                {
                    MethodName = "Vehicle/Update",
                    Message = "verificando input de UpdateVehicle",
                    StackMessage = JsonConvert.SerializeObject(input),
                    Type = "Info"
                });

                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<BaseOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                CreateLog(new CreateLogInput{
                  MethodName = "Vehicle/Update",
                  Message = ex.Message,
                  StackMessage = ex.StackTrace,
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
