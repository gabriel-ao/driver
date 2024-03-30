using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{
    public class GetVehicleOutput : BaseOutput
    {
        public GetVehicleOutput()
        {
            List = new List<GetVehicleItem>();
        }
        public List<GetVehicleItem> List { get; set; }
    }

    public class GetVehicleItem
    {
        public Guid ID { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public string Status { get; set; }
        public string Driver { get; set; }
    }
}
