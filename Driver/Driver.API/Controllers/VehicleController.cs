using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;
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

        [HttpPost("Create")]
        public ActionResult<BaseOutput> CreateVehicle(CreateVehicleInput input)
        {

            var inputModel = new CreateVehicleInputModel()
            {
                Year = input.Year,
                Model = input.Model,
                Plate = input.Plate.Replace(".", "").Replace("-", "").Replace(" ", ""),
                UserId = new Guid("2506519c-a134-4bb5-a3ad-1753d6b60a77")
            };

            var result = _vehicleService.CreateVehicle(inputModel);

            return Ok(result);
        }

        [HttpGet("Get")]
        public ActionResult<GetVehicleOutput> GetVehicle(string? plate)
        {

            if(string.IsNullOrEmpty(plate)) plate = "";

            var inputModel = new GetVehicleInputModel()
            {
                Plate = plate.Replace(".", "").Replace("-", "").Replace(" ", ""),
                UserId = new Guid("2506519c-a134-4bb5-a3ad-1753d6b60a77")
            };

            var result = _vehicleService.GetVehicle(inputModel);

            return Ok(result);
        }

        [HttpPut("Update")]
        public ActionResult<BaseOutput> UpdateVehicle(UpdateVehicleInput input)
        {

            var inputModel = new UpdateVehicleInputModel()
            {
                NewPlate= input.NewPlate.Replace(".", "").Replace("-", "").Replace(" ", ""),
                VehicleId = input.VehicleId,
                UserId = new Guid("2506519c-a134-4bb5-a3ad-1753d6b60a77")
            };

            var result = _vehicleService.UpdateVehicle(inputModel);

            return Ok(result);
        }

        [HttpDelete("Detele")]
        public ActionResult<BaseOutput> DeteleVehicle(DeleteVehicleInput input)
        {
            var inputModel = new DeleteVehicleInputModel()
            {
                VehicleId = input.VehicleId,
                UserId = new Guid("2506519c-a134-4bb5-a3ad-1753d6b60a77")
            };

            var result = _vehicleService.DeleteVehicle(inputModel);

            return Ok(result);
        }

    }
}
