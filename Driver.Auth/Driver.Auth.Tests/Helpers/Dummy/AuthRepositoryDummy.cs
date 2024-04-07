using Driver.Auth.Domain.Interfaces.Repositories;
using Driver.Auth.Domain.Models.Output;

namespace Driver.Auth.Tests.Helpers.Dummy
{
    public class AuthRepositoryDummy : IAuthRepository
    {
        public AuthOutputModal Auth(string useDate, string password)
        {
            return null;
        }
    }
}
