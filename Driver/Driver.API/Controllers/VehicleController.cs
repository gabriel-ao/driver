using Driver.Domain.Helpers;
using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [Authorize("Bearer")]
        [HttpPost("Create")]
        public ActionResult<BaseOutput> CreateVehicle(CreateVehicleInput input)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var inputModel = new CreateVehicleInputModel()
            {
                Year = input.Year,
                Model = input.Model,
                Plate = input.Plate.Replace(".", "").Replace("-", "").Replace(" ", ""),
                UserId = userId
            };

            var result = _vehicleService.CreateVehicle(inputModel);

            return Ok(result);
        }

        [Authorize("Bearer")]
        [HttpGet("Get")]
        public ActionResult<GetVehicleOutput> GetVehicle(string? plate)
        {

            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            if (string.IsNullOrEmpty(plate)) plate = "";

            var inputModel = new GetVehicleInputModel()
            {
                Plate = plate.Replace(".", "").Replace("-", "").Replace(" ", ""),
                UserId = userId
            };

            var result = _vehicleService.GetVehicle(inputModel);

            return Ok(result);
        }

        [Authorize("Bearer")]
        [HttpPut("Update")]
        public ActionResult<BaseOutput> UpdateVehicle(UpdateVehicleInput input)
        {

            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var inputModel = new UpdateVehicleInputModel()
            {
                NewPlate= input.NewPlate.Replace(".", "").Replace("-", "").Replace(" ", ""),
                VehicleId = input.VehicleId,
                UserId = userId
            };

            var result = _vehicleService.UpdateVehicle(inputModel);

            return Ok(result);
        }

        [Authorize("Bearer")]
        [HttpDelete("Detele")]
        public ActionResult<BaseOutput> DeteleVehicle(DeleteVehicleInput input)
        {

            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var inputModel = new DeleteVehicleInputModel()
            {
                VehicleId = input.VehicleId,
                UserId = userId
            };

            var result = _vehicleService.DeleteVehicle(inputModel);

            return Ok(result);
        }

    }
}
