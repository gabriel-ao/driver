using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;
using Driver.Manager.Domain.Models.Output;

namespace Driver.Manager.Domain.Interfaces.Services
{
    public interface IDeliveryService
    {
        BaseOutput PushNotification(PushNotificationInput input);
        CreateDeliveryOrderOutput CreateDeliveryOrder(CreateDeliveryOrderInputModel input);
        GetOrderNotificationsOutput GetOrderNotifications(Guid orderId, Guid userId);
    }
}
