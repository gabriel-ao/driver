﻿using Dapper;
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

        public CreateRentOutput CreateRent(CreateRentInputModel input)
        {
            var response = new CreateRentOutput();

            var QUERY = $"SELECT * FROM \"public\".\"DRV_Create_Rent\"(" +
                $"'{input.PlanId}', " +
                $"'{input.UserId}')";

            try
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Driver/Rent",
                    Message = "verificando input de CreateRent",
                    StackMessage = JsonConvert.SerializeObject(input),
                    Type = "Info"
                });

                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<CreateRentOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Driver/Create/Rent",
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

        public UpdateRentOutput UpdateRent(UpdateRentInputModel input)
        {
            var response = new UpdateRentOutput();

            var QUERY = $"SELECT * FROM \"public\".\"DRV_Update_Rent\"(" +
                $"'{input.PreviousDate.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"'{input.RentId}', " +
                $"'{input.UserId}')";

            try
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Driver/Update/Rent",
                    Message = "verificando input de CreateRent",
                    StackMessage = JsonConvert.SerializeObject(input),
                    Type = "Info"
                });

                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<UpdateRentOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Driver/Rent",
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

        public BaseOutput UpdateCNH(string urlImage, Guid userId)
        {
            var response = new BaseOutput();

            var QUERY = $"SELECT * FROM \"public\".\"DRV_Update_CNH\"(" +
                $"'{urlImage}', " +
                $"'{userId}')";

            try
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Driver/Update/CNH",
                    Message = "verificando input de UpdateRent",
                    StackMessage = "urlImage: " + JsonConvert.SerializeObject(urlImage) + " - userId: " + JsonConvert.SerializeObject(userId),
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
                    MethodName = "Driver/Update/CNH",
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

        public BaseOutput AcceptDeliveryOrder(AcceptDeliveryOrderInputModel input)
        {
            var response = new BaseOutput();

            var QUERY = $"SELECT * FROM \"public\".\"DRV_Accept_Delivery_Order\"(" +
                $"'{input.OrderId}', " +
                $"'{input.UserId}')";

            try
            {
                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<BaseOutput>(QUERY);

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

        public BaseOutput FinishDeliveryOrder(FinishDeliveryOrderInputModel input)
        {
            var response = new BaseOutput();

            var QUERY = $"SELECT * FROM \"public\".\"DRV_Finish_Delivery_Order\"(" +
                $"'{input.OrderId}', " +
                $"'{input.UserId}')";

            try
            {
                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<BaseOutput>(QUERY);

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

        public CnhTypesOutput CnhTypes()
        {
            var response = new CnhTypesOutput();

            var QUERY = $"SELECT \"ID\", \"Description\" FROM \"CnhTypes\"";

            try
            {
                var connection = _connection.GetConnection;
                var result = connection.Query<CnhTypesItem>(QUERY).ToList();

                response.CnhTypes = result;

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Driver/Cnh",
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

        public PlansOutput Plans()
        {
            var response = new PlansOutput();

            var QUERY = $"SELECT \"ID\", \"Days\", \"Price\" FROM \"Plans\"";

            try
            {
                var connection = _connection.GetConnection;
                var result = connection.Query<PlanItem>(QUERY).ToList();

                response.Plans = result;

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Driver/Plans",
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

        public NotificartionDetailsOutput NotificartionDetails(Guid orderId, Guid userId)
        {
            var response = new NotificartionDetailsOutput();

            var QUERY = $"SELECT * FROM \"public\".\"DRV_Notification_Details\"(" +
                $"'{orderId}', " +
                $"'{userId}')";

            try
            {
                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<NotificartionDetailsOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = $"NotificationDetails/{orderId}",
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

        public GetNotificationsOutput GetNotifications(Guid userId)
        {
            var response = new GetNotificationsOutput();

            var QUERY = $"SELECT * FROM \"public\".\"DRV_Get_Notifications\"(" +
                $"'{userId}')";

            try
            {
                var connection = _connection.GetConnection;
                var result = connection.Query<NotificationItem>(QUERY).ToList();

                response.Notifications = result;

                return response;
            }
            catch (Exception ex)
            {
                _logRepository.CreateLog(new CreateLogInput
                {
                    MethodName = "Get/GetNotifications",
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
