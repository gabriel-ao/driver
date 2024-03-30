using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;

namespace Driver.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        BaseOutput CreateLog(CreateLogInput input);
    }
}
