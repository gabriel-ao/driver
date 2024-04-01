namespace Driver.Domain.Models.Input
{
    public class CreateRentInput
    {
        public Guid PlanId { get; set; }
    }

    public class CreateRentInputModel
    {
        public Guid PlanId { get; set; }
        public Guid UserId { get; set; }
    }
}
