using Driver.Manager.Domain.Models.Base;

namespace Driver.Manager.Domain.Models.Output
{
    public class GetOrderNotificationsOutput : BaseOutput
    {
        public GetOrderNotificationsOutput()
        {
            Notifications = new List<NotificationDriverItem>();

        }
        public List<NotificationDriverItem> Notifications { get; set; }

    }

    public class NotificationDriverItem
    {
        public Guid Id { get; set; }
        public string DriverName { get; set; }
        public string CnhNumber { get; set; }
        public bool Read { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
