using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{
    public class CreateRentOutput : BaseOutput
    {
        public Guid RentId { get; set; }
    }
}
