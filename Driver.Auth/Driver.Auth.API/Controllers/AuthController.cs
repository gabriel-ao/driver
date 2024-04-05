using Driver.Auth.Domain.Interfaces.Services;
using Driver.Auth.Domain.Models.Base;
using Driver.Auth.Domain.Models.Configuration;
using Driver.Auth.Domain.Models.Input;
using Driver.Auth.Domain.Models.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Driver.Auth.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOptions<AppitoConfiguration> _config;
        public AuthController(IAuthService authService, IOptions<AppitoConfiguration> config)
        {
            _authService = authService;
            _config = config;
        }

        [HttpPost("Login")]
        public ActionResult<AuthOutput> Auth([FromBody]AuthInput input,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {

            var response = new AuthOutputModal();
            var isValidUser = false;

            if(!String.IsNullOrEmpty(input.UserData) && !String.IsNullOrEmpty(input.Password))
            {
                response = _authService.Auth(input.UserData, input.Password);

                isValidUser = (response.UserID != null);
            }

            if (isValidUser)
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(response.UserID, "UserID"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, response.UserID)
                    }
                );

                var dataCriacao = DateTime.Now;

                var dataExpiracao = dataCriacao +
                 TimeSpan.FromDays(3);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return Ok(new AuthOutput
                {
                    Token = token,
                    Error = false,
                    Message = ""
                });
            }


            return Ok(new AuthOutput
            {
                Error = true,
                Message = "Failed to authenticate"
            });

        }

    }
}
