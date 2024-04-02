using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{
    public class CreateDeliveryOrderOutput : BaseOutput
    {
        public Guid OrderID { get; set; }
    }
}
