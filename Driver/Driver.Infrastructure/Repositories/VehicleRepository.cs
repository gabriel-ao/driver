using Dapper;
using Driver.Domain.Interfaces.Repositories;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;

namespace Driver.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IDapperConnectionFactory _connection;

        public VehicleRepository(IDapperConnectionFactory connection)
        {
            _connection = connection;
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
                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<BaseOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                // TODO CRIAR LOG PARA ERRO NA BASE
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
