﻿using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;

namespace Driver.Domain.Interfaces.Services
{
    public interface IDeliveryService
    {
        BaseOutput PushNotification(PushNotificationInput input);
        CreateDeliveryOrderOutput CreateDeliveryOrder(CreateDeliveryOrderInputModel input);
        GetOrderNotificationsOutput GetOrderNotifications(Guid orderId, Guid userId);
    }
}
