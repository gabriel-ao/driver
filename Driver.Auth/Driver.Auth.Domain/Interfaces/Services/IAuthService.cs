using Driver.Auth.Domain.Models.Output;

namespace Driver.Auth.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        AuthOutputModal Auth(string userDate, string password);
    }
}
