using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;
using Driver.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Driver.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost("Create")]
        public ActionResult<BaseOutput> CreateDriver(CreateDriverInput input)
        {
            var result = _driverService.CreateDriver(input);

            return Ok(result);
        }


        [HttpPost("Create/Rent")]
        public ActionResult<CreateRentOutput> CreateRent(CreateRentInput input)
        {

            var inputModel = new CreateRentInputModel()
            {
                PlanId = input.PlanId,
                UserId = new Guid("6db6a464-fb46-47be-8555-4f3751229e63") // motorista gabriel
            };

            var result = _driverService.CreateRent(inputModel);

            return Ok(result);
        }


        [HttpPut("Update/Rent")]
        public ActionResult<UpdateRentOutput> UpdateRent(UpdateRentInput input)
        {
            //TODO - PASSAR TOKEN
            var inputModel = new UpdateRentInputModel()
            {
                PreviousDate = input.PreviousDate,
                RentId = input.RentId,
                UserId = new Guid("3984508a-d787-4c66-b761-adc98083c5c1") // motorista gabriel
            };

            var result = _driverService.UpdateRent(inputModel);

            return Ok(result);
        }

    }
}
