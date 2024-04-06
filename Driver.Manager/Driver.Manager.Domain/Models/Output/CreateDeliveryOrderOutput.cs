using Driver.Manager.Domain.Models.Base;

namespace Driver.Manager.Domain.Models.Output
{
    public class CreateDeliveryOrderOutput : BaseOutput
    {
        public Guid OrderID { get; set; }
    }
}
