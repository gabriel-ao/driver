using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;

namespace Driver.Manager.Domain.Interfaces.Services
{
    public interface ILogService
    {
        BaseOutput CreateLog(CreateLogInput input);
    }
}
