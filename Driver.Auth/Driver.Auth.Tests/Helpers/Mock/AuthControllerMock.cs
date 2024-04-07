using Driver.Auth.API.Controllers;
using Driver.Auth.Domain.Interfaces.Services;
using Driver.Auth.Domain.Models.Configuration;
using Driver.Auth.Domain.Models.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Auth.Tests.Helpers.Mock
{
    public class AuthControllerMock : AuthController
    {
        public AuthControllerMock(IAuthService authService, IOptions<DriverConfiguration> config) : base(authService, config)
        {

        }
        public string GenerateToken(string userId, [FromServices] TokenConfigurations tokenConfigurations, [FromServices] SigningConfigurations signingConfigurations)
        {
            var identity = new ClaimsIdentity(
                new GenericIdentity(userId, "UserID"),
                new[] {
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                    new Claim(JwtRegisteredClaimNames.UniqueName, userId)
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

            return token;
        }


    }
}
