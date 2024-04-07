using Driver.Auth.API.Controllers;
using Driver.Auth.Domain.Interfaces.Services;
using Driver.Auth.Domain.Models.Configuration;
using Driver.Auth.Tests.Helpers.Mock;
using Microsoft.Extensions.Options;
using Moq;

namespace Driver.Auth.Tests.Controller
{
    public class AuthControllerTests
    {
        [Fact]
        public void GenerateToken()
        {
            var authServiceMock = new Mock<IAuthService>();
            var optionsMock = new Mock<IOptions<DriverConfiguration>>();

            var signingConfigurations = new SigningConfigurations();

            var tokenConfigurations = new TokenConfigurations
            {
                Audience = "driverAuth",
                Issuer = "http://myapi.com",
                Seconds = 259200
            };

            optionsMock.Setup(opt => opt.Value).Returns(new DriverConfiguration());

            var authController = new AuthControllerMock(authServiceMock.Object, optionsMock.Object);
            var userId = Guid.NewGuid().ToString();

            string token = authController.GenerateToken(userId, tokenConfigurations, signingConfigurations);

            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }
    }
}
