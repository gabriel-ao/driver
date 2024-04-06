using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{

    public class PlansOutput : BaseOutput
    {
        public PlansOutput()
        {
            Plans = new List<PlanItem>();
        }
        public List<PlanItem> Plans { get; set; }
    }

    public class PlanItem
    {
        public Guid ID { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
    }
}
