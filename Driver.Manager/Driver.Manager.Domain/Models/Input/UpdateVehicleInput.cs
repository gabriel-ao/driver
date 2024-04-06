namespace Driver.Manager.Domain.Models.Input
{
    public class UpdateVehicleInput
    {
        public string NewPlate { get; set; }
        public Guid VehicleId { get; set; }
    }

    public class UpdateVehicleInputModel
    {
        public string NewPlate { get; set; }
        public Guid VehicleId { get; set; }
        public Guid UserId { get; set; }
    }
}
