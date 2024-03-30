using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;

namespace Driver.Domain.Interfaces.Services
{
    public interface IVehicleService
    {
        BaseOutput CreateVehicle(CreateVehicleInputModel input);
        GetVehicleOutput GetVehicle(GetVehicleInputModel input);
        BaseOutput UpdateVehicle(UpdateVehicleInputModel input);
        BaseOutput DeleteVehicle(DeleteVehicleInputModel input);
    }
}
