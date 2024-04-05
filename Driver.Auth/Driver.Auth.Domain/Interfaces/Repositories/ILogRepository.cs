using Driver.Auth.Domain.Models.Base;
using Driver.Auth.Domain.Models.Input;
using Driver.Auth.Domain.Models.Output;

namespace Driver.Auth.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        BaseOutput CreateLog(CreateLogInput input);
    }
}
