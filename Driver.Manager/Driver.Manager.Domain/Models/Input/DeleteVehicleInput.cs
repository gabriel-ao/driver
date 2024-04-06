namespace Driver.Manager.Domain.Models.Input
{
    public class DeleteVehicleInput
    {
        public Guid VehicleId { get; set; }
    }

    public class DeleteVehicleInputModel
    {
        public Guid VehicleId { get; set; }
        public Guid UserId { get; set; }
    }
}
