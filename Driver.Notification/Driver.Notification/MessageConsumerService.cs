using Dapper;
using Driver.Notification.DatabaseConnection;
using Driver.Notification.Models.Base;
using Driver.Notification.Models.Input;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Driver.Notification
{
    public class MessageConsumerService
    {

        public void ConsumeAvailableOrders()
        {
            var _connection = new DapperConnectionFactory();

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "disponible_order",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var input = System.Text.Json.JsonSerializer.Deserialize<PushNotificationInput>(message);

                try
                {
                    RegisterNotification(input, _connection);

                    channel.BasicAck(ea.DeliveryTag, false); // Confirms receipt of the message only after processing is complete
                }
                catch (Exception ex)
                {
                    channel.BasicNack(ea.DeliveryTag, false, true); // Reject the message and requeue for another consumer

                    CreateLog(new CreateLogInput
                    {
                        MethodName = "ConsumeAvailableOrders",
                        Message = ex.Message,
                        StackMessage = ex.StackTrace,
                        Type = "Error"
                    }, _connection);
                }
            };
            channel.BasicConsume(queue: "disponible_order",
                                 autoAck: false, // Disable o autoAck
                                 consumer: consumer);
        }


        public void RegisterNotification(PushNotificationInput input, DapperConnectionFactory _connection)
        {
            Console.WriteLine($"==============================================");
            Console.WriteLine($"register notification rabbit == {DateTime.Now}");
            Console.WriteLine($"==============================================");

            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // Optional: to keep formatting indented for easier reading
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var jsonString = System.Text.Json.JsonSerializer.Serialize<PushNotificationInput>(input, options);

                CreateLog(new CreateLogInput
                {
                    MethodName = "ConsumeAvailableOrders",
                    Message = "verificando input de ConsumeAvailableOrders",
                    StackMessage = jsonString,
                    Type = "Info"
                }, _connection);


                if (input.Rents.Count > 0)
                {
                    CreateDeliveryNofitication(input, _connection);
                }

            }
            catch (Exception ex)
            {

                CreateLog(new CreateLogInput
                {
                    MethodName = "ConsumeAvailableOrders",
                    Message = ex.Message,
                    StackMessage = ex.StackTrace,
                    Type = "Error"
                }, _connection);

            }
        }


        public BaseOutput CreateLog(CreateLogInput input, DapperConnectionFactory _connection)
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



        public BaseOutput CreateDeliveryNofitication(PushNotificationInput input, DapperConnectionFactory _connection)
        {
            var response = new BaseOutput();

            try
            {
                var rents = "{";
                foreach (var rent in input.Rents)
                {
                    rents += rent.RentID;
                };
                rents += "}";

                var QUERY = $"SELECT * FROM \"public\".\"SIS_Create_Delivery_Nofitication\"(" +
                    $"'{input.OrderId}', " +
                    $"'{input.Title}', " +
                    $"'{input.Description}', " +
                    $"'{rents}')";

                var connection = _connection.GetConnection;
                response = connection.QueryFirstOrDefault<BaseOutput>(QUERY);

                return response;
            }
            catch (Exception ex)
            {

                CreateLog(new CreateLogInput
                {
                    MethodName = "ConsumeAvailableOrders",
                    Message = JsonConvert.SerializeObject(ex.Message).Replace("'", "´"),
                    StackMessage = JsonConvert.SerializeObject(ex.Message).Replace("'", "´"),
                    Type = "Error"
                }, _connection);


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
