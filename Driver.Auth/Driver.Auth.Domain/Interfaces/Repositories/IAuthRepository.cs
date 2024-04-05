using Driver.Auth.Domain.Models.Output;

namespace Driver.Auth.Domain.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        AuthOutputModal Auth(string useDate, string password);
    }
}
