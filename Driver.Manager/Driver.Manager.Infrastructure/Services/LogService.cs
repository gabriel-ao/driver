using Driver.Manager.Domain.Interfaces.Repositories;
using Driver.Manager.Domain.Interfaces.Services;
using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Manager.Infrastructure.Services
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
