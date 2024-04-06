using Dapper;
using Driver.Manager.Domain.Interfaces.Repositories;
using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;
using Driver.Manager.Domain.Models.Output;
using Newtonsoft.Json;

namespace Driver.Manager.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDapperConnectionFactory _connection;
        private readonly ILogRepository _logRepository;
        public VehicleRepository(IDapperConnectionFactory connection, ILogRepository logRepository)
        {
            _connection = connection;
            _logRepository = logRepository;
        }


        public BaseOutput CreateVehicle(CreateVehicleInputModel input)
        {
            var response = new BaseOutput();

            var QUERY = $"SELECT * FROM \"public\".\"ADM_Create_Vehicle\"(" +
                $"'{input.Year}', " +
                $"'{input.Model}', " +
                $"'{input.Plate}', " +
                $"'{input.UserId}')";

            try
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Vehicle/Create",
                    Message = "verificando input de CreateVehicle",
                    StackMessage = JsonConvert.SerializeObject(input),
                    Type = "Info"
                });

                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<BaseOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Vehicle/Create",
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

        public GetVehicleOutput GetVehicle(GetVehicleInputModel input)
        {
            var response = new GetVehicleOutput();

            var QUERY = $"SELECT * FROM \"public\".\"ADM_Get_Vehicles\"(" +
                $"'{input.Plate}', " +
                $"'{input.UserId}')";

            try
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Vehicle/Get",
                    Message = "verificando input de UpdateVehicle",
                    StackMessage = JsonConvert.SerializeObject(input),
                    Type = "Info"
                });

                var connection = _connection.GetConnection;
                var result = connection.Query<GetVehicleItem>(QUERY).ToList();

                response.Vehicles = result;

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Vehicle/Get",
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

        public BaseOutput UpdateVehicle(UpdateVehicleInputModel input)
        {
            var response = new BaseOutput();

            var QUERY = $"SELECT * FROM \"public\".\"ADM_Update_Vehicle\"(" +
                $"'{input.NewPlate}', " +
                $"'{input.VehicleId}', " +
                $"'{input.UserId}')";

            try
            {
                _logRepository.CreateLog(new CreateLogInput
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
                _logRepository.CreateLog(new CreateLogInput
                {
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

        public BaseOutput DeleteVehicle(DeleteVehicleInputModel input)
        {
            var response = new BaseOutput();

            var QUERY = $"SELECT * FROM \"public\".\"ADM_Delete_Vehicle\"(" +
                $"'{input.VehicleId}', " +
                $"'{input.UserId}')";

            try
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Vehicle/Delete",
                    Message = "verificando input de DeleteVehicle",
                    StackMessage = JsonConvert.SerializeObject(input),
                    Type = "Info"
                });

                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<BaseOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Vehicle/Delete",
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
