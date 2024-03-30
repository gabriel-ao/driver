using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Microsoft.AspNetCore.Mvc;

namespace Driver.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPut("Vehicle/Update")]
        public ActionResult<BaseOutput> CreateUser(UpdateVehicleInput input)
        {

            var inputModel = new UpdateVehicleInputModel()
            {
                NewPlate= input.NewPlate,
                VehicleId = input.VehicleId,
                UserId = new Guid("2506519c-a134-4bb5-a3ad-1753d6b60a77")
            };

            var result = _vehicleService.UpdateVehicle(inputModel);

            return Ok(result);
        }

    }
}
