using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{
    public class CreateDriverOutput : BaseOutput
    {

    }

    public class CreateDriverOutputModel : BaseOutput
    {
        public Guid UserId { get; set; }
    }
}
