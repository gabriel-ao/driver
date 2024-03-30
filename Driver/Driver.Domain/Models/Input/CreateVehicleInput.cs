namespace Driver.Domain.Models.Input
{
    public class CreateVehicleInput
    {
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }

    public class CreateVehicleInputModel
    {
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public Guid UserId { get; set; }
    }
}
