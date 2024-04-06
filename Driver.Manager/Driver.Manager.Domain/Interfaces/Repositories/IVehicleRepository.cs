using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;
using Driver.Manager.Domain.Models.Output;

namespace Driver.Manager.Domain.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        BaseOutput CreateVehicle(CreateVehicleInputModel input);
        GetVehicleOutput GetVehicle(GetVehicleInputModel input);
        BaseOutput UpdateVehicle(UpdateVehicleInputModel input);
        BaseOutput DeleteVehicle(DeleteVehicleInputModel input);

    }
}
