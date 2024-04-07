using Driver.Auth.Domain.Models.Input;

namespace Driver.Auth.Tests.Domain
{
    public class AuthLogin
    {
        [Fact]
        public void ManagerObject()
        {
            var email = "gabriel-ao@hotmail.com";
            var password = "1234";

            var input = new AuthInput();

            input.UserData = email;
            input.Password = password;

            Assert.Equal(email, input.UserData);
            Assert.Equal(password, input.Password);
        }

        [Fact]
        public void DriverObject()
        {
            var email = "12121212";
            var password = "1234";

            var input = new AuthInput();

            input.UserData = email;
            input.Password = password;

            Assert.Equal(email, input.UserData);
            Assert.Equal(password, input.Password);
        }

        //[Fact]
        //public void SuccessLogin()
        //{
        //    var email = "gabriel-ao@hotmail.com";
        //    var password = "1234";

        //    var input = new AuthInput();

        //    input.UserData = email;
        //    input.Password = password;

        //    Assert.Equal(email, input.UserData);
        //    Assert.Equal(password, input.Password);


        //}
    }
}