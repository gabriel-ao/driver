using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;

namespace Driver.Domain.Interfaces.Services
{
    public interface ILogService
    {
        BaseOutput CreateLog(CreateLogInput input);
    }
}
