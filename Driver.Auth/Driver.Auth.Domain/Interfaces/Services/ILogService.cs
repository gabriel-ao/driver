using Driver.Auth.Domain.Models.Base;
using Driver.Auth.Domain.Models.Input;

namespace Driver.Auth.Domain.Interfaces.Services
{
    public interface ILogService
    {
        BaseOutput CreateLog(CreateLogInput input);
    }
}
