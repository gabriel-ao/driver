using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;
using Driver.Manager.Domain.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Manager.Domain.Interfaces.Services
{
    public interface IVehicleService
    {
        BaseOutput CreateVehicle(CreateVehicleInputModel input);
        GetVehicleOutput GetVehicle(GetVehicleInputModel input);
        BaseOutput UpdateVehicle(UpdateVehicleInputModel input);
        BaseOutput DeleteVehicle(DeleteVehicleInputModel input);
    }
}
