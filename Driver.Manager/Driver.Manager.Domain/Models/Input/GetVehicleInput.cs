namespace Driver.Manager.Domain.Models.Input
{
    public class GetVehicleInput
    {
        public string Plate { get; set; }
    }

    public class GetVehicleInputModel
    {
        public string Plate { get; set; }
        public Guid UserId { get; set; }
    }
}
