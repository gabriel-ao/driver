using Driver.Manager.Domain.Interfaces.Repositories;
using Driver.Manager.Domain.Interfaces.Services;
using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;
using Driver.Manager.Domain.Models.Output;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Driver.Manager.Infrastructure.Services
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

            return new BaseOutput { Error = false, Message = "" };
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

        public GetOrderNotificationsOutput GetOrderNotifications(Guid orderId, Guid userId)
        {
            return _deliveryRepository.GetOrderNotifications(orderId, userId);
        }
    }
}
