using Driver.Domain.Interfaces.Repositories;
using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;

namespace Driver.Infrastructure.Services
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
