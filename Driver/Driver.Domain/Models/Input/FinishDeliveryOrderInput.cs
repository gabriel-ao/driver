namespace Driver.Domain.Models.Input
{
    public class FinishDeliveryOrderInput
    {
        public Guid OrderId { get; set; }
    }

    public class FinishDeliveryOrderInputModel
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }

    }

}
