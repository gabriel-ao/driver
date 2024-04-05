using Driver.Auth.Domain.Models.Base;

namespace Driver.Auth.Domain.Models.Output
{
    public class AuthOutput : BaseOutput
    {
        public string Token { get; set; }
    }
    public class AuthOutputModal : BaseOutput
    {
        public string UserID { get; set; }
    }

}
