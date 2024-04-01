namespace Driver.Domain.Models.Input
{
    public class UpdateRentInput
    {
        public DateTime PreviousDate { get; set; }
        public Guid RentId { get; set; }
    }

    public class UpdateRentInputModel
    {
        public DateTime PreviousDate { get; set; }
        public Guid RentId { get; set; }
        public Guid UserId { get; set; }
    }
}
