using Driver.Manager.Domain.Models.Input;
using Driver.Manager.Domain.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Manager.Domain.Interfaces.Repositories
{
    public interface IDeliveryRepository
    {
        CreateDeliveryOrderOutput CreateDeliveryOrder(CreateDeliveryOrderInputModel input);
        GetAvailableDriversOutput GetAvailableDrivers(Guid UserId);
        GetOrderNotificationsOutput GetOrderNotifications(Guid orderId, Guid userId);
    }
}
