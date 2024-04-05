using Driver.Auth.Domain.Interfaces.Repositories;
using Driver.Auth.Domain.Interfaces.Services;
using Driver.Auth.Domain.Models.Output;

namespace Driver.Auth.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDapperConnectionFactory _connection;
        private readonly IAuthRepository _authRepository;

        public AuthService(IDapperConnectionFactory connection, IAuthRepository authRepository)
        {
            _connection = connection;
            _authRepository = authRepository;
        }

        public AuthOutputModal Auth(string userDate, string password)
        {
            // TODO - VALIDAR OS CAMPOS
            return _authRepository.Auth(userDate, password);
        }
    }
}
