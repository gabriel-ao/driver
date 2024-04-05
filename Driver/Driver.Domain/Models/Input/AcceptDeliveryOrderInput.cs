namespace Driver.Domain.Models.Input
{
    public class AcceptDeliveryOrderInput
    {
        public Guid OrderId { get; set; }
    }
    public class AcceptDeliveryOrderInputModel
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
    }
}
