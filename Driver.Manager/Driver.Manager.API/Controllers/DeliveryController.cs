﻿using Driver.Manager.Domain.Helpers;
using Driver.Manager.Domain.Interfaces.Services;
using Driver.Manager.Domain.Models.Input;
using Driver.Manager.Domain.Models.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Driver.Manager.API.Controllers
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

        [Authorize("Bearer")]
        [HttpPost("Create")]
        public ActionResult<CreateDeliveryOrderOutput> CreateDeliveryOrder(CreateDeliveryOrderInput input)
        {

            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var inputModel = new CreateDeliveryOrderInputModel()
            {
                Title = input.Title,
                Description = input.Description,
                Price = input.Price,
                UserId = userId
            };

            var result = _deliveryService.CreateDeliveryOrder(inputModel);

            return Ok(result);
        }

        [Authorize("Bearer")]
        [HttpGet("Notifications/{orderId}")]
        public ActionResult<GetOrderNotificationsOutput> GetOrderNotifications(Guid orderId)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var result = _deliveryService.GetOrderNotifications(orderId, userId);

            return Ok(result);
        }

    }
}
