using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{
    public class UpdateRentOutput : BaseOutput
    {
        public DateTime FinishDate { get; set; }
        public decimal Price { get; set; }
    }
}
