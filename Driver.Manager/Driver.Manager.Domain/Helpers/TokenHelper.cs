using System.IdentityModel.Tokens.Jwt;

namespace Driver.Manager.Domain.Helpers
{
    public static class TokenHelper
    {
        public static Guid GetUserId(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                var jti = tokenS.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Name).Value;
                Guid.TryParse(jti, out var id);
                return id;
            }
            catch (Exception ex)
            {
                return Guid.Parse("00000000-0000-0000-0000-000000000000"); ;
            }
        }
    }
}
