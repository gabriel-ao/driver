using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;

namespace Driver.Domain.Interfaces.Repositories
{
    public interface IDriverRepository
    {
        CreateDriverOutputModel CreateDriver(CreateDriverInput input);
    }
}
