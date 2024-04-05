using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;
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

        [HttpPost("Create")]
        public ActionResult<CreateDeliveryOrderOutput> CreateDeliveryOrder(CreateDeliveryOrderInput input)
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

        [HttpGet("Notifications/{orderId}")]
        public ActionResult<GetOrderNotificationsOutput> GetOrderNotifications(Guid orderId)
        {
            var userId = new Guid("2506519c-a134-4bb5-a3ad-1753d6b60a77");

            var result = _deliveryService.GetOrderNotifications(orderId, userId);

            return Ok(result);
        }

    }
}
