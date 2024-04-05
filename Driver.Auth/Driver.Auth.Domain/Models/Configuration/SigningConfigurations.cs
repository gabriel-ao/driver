using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Auth.Domain.Models.Configuration
{
    public class SigningConfigurations
    {
        const string sec = "C27B35306C510E8A06A267BDB908E8BC7C7BD290AB7CF4562F24F3C35AFF4001";

        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {

            Key = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

        }
    }
}
