using Driver.Domain.Interfaces.Repositories;
using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;

namespace Driver.Infrastructure.Services
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
