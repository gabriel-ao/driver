using Driver.Auth.Domain.Models.Input;
using Driver.Auth.Tests.Helpers.Dummy;
using Driver.Auth.Tests.Helpers.Stub;

namespace Driver.Auth.Tests.Repository
{
    public class AuthRepositoryTests
    {
        [Fact]
        public async void LoginAuthInvalid()
        {
            var email = "gabriel-ao@hotmail.com";
            var password = "1234";

            var input = new AuthInput();

            input.UserData = email;
            input.Password = password;

            var repository = new AuthRepositoryDummy();

            var result = repository.Auth(input.UserData, input.Password);

            Assert.Null(result);
        }

        [Fact]
        public async void LoginAuthValid()
        {
            var email = "gabriel-ao@hotmail.com";
            var password = "1234";

            var input = new AuthInput();

            input.UserData = email;
            input.Password = password;

            var repository = new AuthRepositoryMock();

            var result = repository.Auth(input.UserData, input.Password);

            Assert.NotNull(result);
        }

    }
}
