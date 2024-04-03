using Driver.Domain.Interfaces.Repositories;
using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Driver.Infrastructure.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ILogRepository _logRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        public DeliveryService(ILogRepository logRepository, IDeliveryRepository deliveryRepository)
        {
            _logRepository = logRepository;
            _deliveryRepository = deliveryRepository;
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

                    _logRepository.CreateLog(new CreateLogInput
                    {
                        MethodName = "ConsumeAvailableOrders",
                        Message = "verificando input de ConsumeAvailableOrders",
                        StackMessage = message,
                        Type = "Info"
                    });

                };
                channel.BasicConsume(queue: "disponible_order",
                                     autoAck: true,
                                     consumer: consumer);
            }
        }

        public CreateDeliveryOrderOutput CreateDeliveryOrder(CreateDeliveryOrderInputModel input)
        {
            var response = new CreateDeliveryOrderOutput();

            response = _deliveryRepository.CreateDeliveryOrder(input);

            if (response.OrderID != null)
            {
                var availableDrivers = _deliveryRepository.GetAvailableDrivers(input.UserId);

                if (availableDrivers.Rents.Count > 0)
                {
                    var rabbit = new PushNotificationInput();

                    var list = new List<RentItem>();

                    foreach (var rent in availableDrivers.Rents)
                    {
                        list.Add(new RentItem
                        {
                            RentID = rent.RentID
                        });
                    }

                    rabbit.Rents = list;
                    rabbit.Title = input.Title;
                    rabbit.Description = input.Description;
                    rabbit.OrderId = response.OrderID;

                    PushNotification(rabbit);
                }
            }

            return response;
        }
    }
}
