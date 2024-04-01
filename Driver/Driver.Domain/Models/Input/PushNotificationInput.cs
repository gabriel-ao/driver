using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Domain.Models.Input
{
    public class PushNotificationInput
    {
        public Guid RentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
