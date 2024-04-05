using Driver.Auth.Domain.Interfaces.Repositories;
using Driver.Auth.Domain.Interfaces.Services;
using Driver.Auth.Domain.Models.Base;
using Driver.Auth.Domain.Models.Input;

namespace Driver.Auth.Infrastructure.Repositories
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _LogRepository;

        public LogService(ILogRepository logRepository)
        {
            _LogRepository = logRepository;
        }
        public BaseOutput CreateLog(CreateLogInput input)
        {
            return _LogRepository.CreateLog(input);
        }
    }
}
