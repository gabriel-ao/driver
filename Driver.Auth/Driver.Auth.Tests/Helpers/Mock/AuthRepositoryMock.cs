using Driver.Auth.Domain.Interfaces.Repositories;
using Driver.Auth.Domain.Models.Output;

namespace Driver.Auth.Tests.Helpers.Stub
{
    public class AuthRepositoryMock : IAuthRepository
    {
        public AuthOutputModal Auth(string useDate, string password)
        {
            var response = new AuthOutputModal();

            response.UserID = Guid.NewGuid().ToString();

            return response;

        }
    }
}
