using Driver.Domain.Interfaces.Repositories;
using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Driver.Infrastructure.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ILogRepository _logRepository;

        public DeliveryService(ILogRepository logRepository)
        {

            _logRepository = logRepository;
        }
        public BaseOutput PushNotification(PushNotificationInput input)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "disponible_order",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(input));

                channel.BasicPublish(exchange: "",
                                     routingKey: "disponible_order",
                                     basicProperties: null,
                                     body: body);
            }

            return new BaseOutput { Error = false, Message = "Notificação enviada com sucesso." };
        }


        public void ConsumeAvailableOrders()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
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
                    var input = JsonSerializer.Deserialize<PushNotificationInput>(message);

                    //TODO - CRIAR PROC PARA ATUALIZAR PEDIDOS
                    //using (var dbConnection = new SqlConnection(_connectionString))
                    //{
                    //    string sql = "INSERT INTO Pedidos (RentID, Title, Description) VALUES (@RentID, @Title, @Description)";
                    //    dbConnection.Execute(sql, input);
                    //}
                };
                channel.BasicConsume(queue: "disponible_order",
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}
