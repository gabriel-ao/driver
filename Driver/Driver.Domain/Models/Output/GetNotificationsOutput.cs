using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{
    public class GetNotificationsOutput : BaseOutput
    {
        public GetNotificationsOutput()
        {
            Notifications = new List<NotificationItem>();
        }

        public List<NotificationItem> Notifications { get; set; }
    }

    public class NotificationItem
    {
        public Guid OrderID { get; set; }
        public string Title { get; set; }
        public string DeliveryStatus { get; set; }
        public bool Read { get; set; }
        public DateTime CreateDate { get; set; }
    }

}
