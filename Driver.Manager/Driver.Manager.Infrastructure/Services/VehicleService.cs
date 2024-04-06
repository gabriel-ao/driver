using Driver.Manager.Domain.Interfaces.Repositories;
using Driver.Manager.Domain.Interfaces.Services;
using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;
using Driver.Manager.Domain.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Manager.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public BaseOutput CreateVehicle(CreateVehicleInputModel input)
        {
            return _vehicleRepository.CreateVehicle(input);
        }

        public GetVehicleOutput GetVehicle(GetVehicleInputModel input)
        {
            return _vehicleRepository.GetVehicle(input);
        }

        public BaseOutput UpdateVehicle(UpdateVehicleInputModel input)
        {
            return _vehicleRepository.UpdateVehicle(input);
        }
        public BaseOutput DeleteVehicle(DeleteVehicleInputModel input)
        {
            return _vehicleRepository.DeleteVehicle(input);
        }
    }
}
