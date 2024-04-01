using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;

namespace Driver.Domain.Interfaces.Services
{
    public interface IDeliveryService
    {
        BaseOutput PushNotification(PushNotificationInput input);
        void ConsumeAvailableOrders();
    }
}
