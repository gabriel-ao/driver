using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;

namespace Driver.Domain.Interfaces.Services
{
    public interface IVehicleService
    {
        BaseOutput UpdateVehicle(UpdateVehicleInputModel input);
    }
}
