namespace Driver.Domain.Models.Input
{
    public class CreateDeliveryOrderInput
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    public class CreateDeliveryOrderInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid UserId { get; set; }
    }

}
