using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{
    public class NotificartionDetailsOutput : BaseOutput
    {
        public Guid OrderID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
