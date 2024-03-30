using Driver.Domain.Interfaces.Repositories;
using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;

namespace Driver.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public BaseOutput CreateLog(CreateLogInput input)
        {
            return _vehicleRepository.CreateLog(input);
        }

        public BaseOutput UpdateVehicle(UpdateVehicleInputModel input)
        {
            return _vehicleRepository.UpdateVehicle(input);
        }
    }
}
