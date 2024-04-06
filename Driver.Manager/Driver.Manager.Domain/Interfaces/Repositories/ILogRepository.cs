using Driver.Manager.Domain.Models.Base;
using Driver.Manager.Domain.Models.Input;

namespace Driver.Manager.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        BaseOutput CreateLog(CreateLogInput input);
    }
}
