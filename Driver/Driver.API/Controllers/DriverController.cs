using Driver.Domain.Helpers;
using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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

        [Authorize("Bearer")]
        [HttpPost("Create/Rent")]
        public ActionResult<CreateRentOutput> CreateRent(CreateRentInput input)
        {

            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var inputModel = new CreateRentInputModel()
            {
                PlanId = input.PlanId,
                UserId = userId
            };

            var result = _driverService.CreateRent(inputModel);

            return Ok(result);
        }

        [Authorize("Bearer")]
        [HttpPut("Update/Rent")]
        public ActionResult<UpdateRentOutput> UpdateRent(UpdateRentInput input)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var inputModel = new UpdateRentInputModel()
            {
                PreviousDate = input.PreviousDate,
                RentId = input.RentId,
                UserId = userId
            };

            var result = _driverService.UpdateRent(inputModel);

            return Ok(result);
        }

        [Authorize("Bearer")]
        [HttpPut("Update/CNH")]
        public async Task<IActionResult> UpdateCNH(IFormFile documentImage)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var response = new SaveDocumentImageOutput();

            var result = await SaveDocumentImage(documentImage, userId);

            if (!string.IsNullOrEmpty(result.urlImage))
            {
                var updateImage = _driverService.UpdateCNH(result.urlImage, userId);

                response.Error = updateImage.Error;
                response.Message = updateImage.Message;
            }
            else
            {
                response.Error = result.Error;
                response.Message = result.Message;
            }

            return Ok(response);
        }

        private async Task<SaveDocumentImageOutputModel> SaveDocumentImage(IFormFile documentImage, Guid userId)
        {

            var response = new SaveDocumentImageOutputModel();

            var fouldName = "Driver-Documents";

            if (documentImage is null)
            {
                response.Error = true;
                response.Message = "Image not found.";
            }

            try
            {

                if (documentImage.ContentType != "image/png" && documentImage.ContentType != "image/bmp")
                {
                    // Convert imagem to PNG
                    using (var bitmap = new Bitmap(documentImage.OpenReadStream()))
                    {
                        var memoryStream = new MemoryStream();
                        bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        documentImage = new FormFile(memoryStream, 0, memoryStream.Length, documentImage.Name, $"{documentImage}-{userId}.png");
                    }
                }

                // create folder
                string pastaFotos = Path.Combine(Directory.GetCurrentDirectory(), fouldName, userId.ToString());
                if (!Directory.Exists(pastaFotos))
                    Directory.CreateDirectory(pastaFotos);


                string imageAway = Path.Combine(pastaFotos, $"CNH-{userId.ToString()}.png");

                response.urlImage = imageAway;

                // save image in disc
                using (var stream = new FileStream(imageAway, FileMode.Create))
                {
                    await documentImage.CopyToAsync(stream);
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Error = true;
                response.urlImage = "";

                return response;
            }
        }

        [Authorize("Bearer")]
        [HttpPost("AcceptDeliveryOrder")]
        public ActionResult<BaseOutput> AcceptDeliveryOrder(AcceptDeliveryOrderInput input)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var inputModel = new AcceptDeliveryOrderInputModel()
            {
                OrderId = input.OrderId,
                UserId = userId
            };

            var result = _driverService.AcceptDeliveryOrder(inputModel);

            return Ok(result);
        }

        [Authorize("Bearer")]
        [HttpPut("FinishDeliveryOrder")]
        public ActionResult<BaseOutput> FinishDeliveryOrder(FinishDeliveryOrderInput input)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "").Replace("bearer ", "");
            var userId = TokenHelper.GetUserId(token);

            var inputModel = new FinishDeliveryOrderInputModel()
            {
                OrderId = input.OrderId,
                UserId = userId
            };

            var result = _driverService.FinishDeliveryOrder(inputModel);

            return Ok(result);
        }

    }
}
