using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
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

        [HttpPost("Driver/Create")]
        public ActionResult<BaseOutput> CreateDriver(CreateDriverInput input)
        {
            var result = _driverService.CreateDriver(input);

            return Ok(result);
        }
    }
}
