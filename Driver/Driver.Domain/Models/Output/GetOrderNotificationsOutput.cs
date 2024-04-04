using Driver.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Domain.Models.Output
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
