using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;

namespace Driver.Domain.Interfaces.Services
{
    public interface IVehicleService
    {
        BaseOutput CreateLog(CreateLogInput input);
        BaseOutput UpdateVehicle(UpdateVehicleInputModel input);
    }
}
