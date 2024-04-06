using Driver.Manager.Domain.Models.Base;

namespace Driver.Manager.Domain.Models.Output
{
    public class GetVehicleOutput : BaseOutput
    {
        public GetVehicleOutput()
        {
            Vehicles = new List<GetVehicleItem>();
        }
        public List<GetVehicleItem> Vehicles { get; set; }
    }

    public class GetVehicleItem
    {
        public Guid ID { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public string Status { get; set; }
        public string DriverName { get; set; }
        public string DriverCnh { get; set; }
    }
}
