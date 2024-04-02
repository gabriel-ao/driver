using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Driver.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPost("PushNotification")]
        public ActionResult<BaseOutput> PushNotification(PushNotificationInput input)
        {
            var rabbit = new PushNotificationInput
            {
                RentID = Guid.NewGuid(),
                Title = "Novo pedido disponível",
                Description = "Um novo pedido está disponível para entrega."
            };
            var result = _deliveryService.PushNotification(rabbit);

            // Para consumir os pedidos disponíveis:
            _deliveryService.ConsumeAvailableOrders();

            //var result = _deliveryService.PushNotification(input);
            return Ok(result);
        }


        [HttpPost("Create")]
        public ActionResult<BaseOutput> CreateDeliveryOrder(CreateDeliveryOrderInput input)
        {
            var inputModel = new CreateDeliveryOrderInputModel()
            {
                Title = input.Title,
                Description = input.Description,
                Price = input.Price,
                UserId = new Guid("2506519c-a134-4bb5-a3ad-1753d6b60a77")
            };

            var result = _deliveryService.CreateDeliveryOrder(inputModel);

            return Ok(result);
        }

    }
}
