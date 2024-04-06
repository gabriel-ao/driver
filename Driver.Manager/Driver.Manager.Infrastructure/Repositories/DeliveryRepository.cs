using Dapper;
using Driver.Manager.Domain.Interfaces.Repositories;
using Driver.Manager.Domain.Models.Input;
using Driver.Manager.Domain.Models.Output;
using Newtonsoft.Json;
using System.Globalization;

namespace Driver.Manager.Infrastructure.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly IDapperConnectionFactory _connection;
        private readonly ILogRepository _logRepository;

        public DeliveryRepository(IDapperConnectionFactory connection, ILogRepository logRepository)
        {
            _connection = connection;
            _logRepository = logRepository;
        }

        public CreateDeliveryOrderOutput CreateDeliveryOrder(CreateDeliveryOrderInputModel input)
        {
            var response = new CreateDeliveryOrderOutput();

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            var QUERY = $"SELECT * FROM \"public\".\"ADM_Create_Delivery_Order\"(" +
                $"'{input.Title}', " +
                $"'{input.Description}', " +
                $"'{input.Price}', " +
                $"'{input.UserId}')";

            try
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Create/Delivery",
                    Message = "verificando input de CreateDelivery",
                    StackMessage = JsonConvert.SerializeObject(input),
                    Type = "Info"
                });

                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<CreateDeliveryOrderOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Create/Delivery",
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

        public GetAvailableDriversOutput GetAvailableDrivers(Guid userId)
        {
            var response = new GetAvailableDriversOutput();

            var QUERY = $"SELECT * FROM \"public\".\"ADM_Get_Available_Drivers\"(" +
                $"'{userId}')";

            try
            {
                var connection = _connection.GetConnection;
                var result = connection.Query<GetAvailableDriverItem>(QUERY).ToList();
                response.Rents = result;

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Create/Delivery",
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

        public GetOrderNotificationsOutput GetOrderNotifications(Guid orderId, Guid userId)
        {
            var response = new GetOrderNotificationsOutput();

            var QUERY = $"SELECT * FROM \"public\".\"ADM_Get_Order_Notifications\"(" +
                $"'{orderId}', " +
                $"'{userId}')";

            try
            {
                var connection = _connection.GetConnection;
                var result = connection.Query<NotificationDriverItem>(QUERY).ToList();
                response.Notifications = result;

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = $"Notifications/{orderId}",
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
