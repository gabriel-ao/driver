using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;

namespace Driver.Domain.Interfaces.Repositories
{
    public interface IDeliveryRepository
    {
        CreateDeliveryOrderOutput CreateDeliveryOrder(CreateDeliveryOrderInputModel input);
        GetAvailableDriversOutput GetAvailableDrivers(Guid UserId);
    }
}
