using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Notification.Models.Input
{
    public class CreateLogInput
    {
        public string MethodName { get; set; }
        public string Message { get; set; }
        public string StackMessage { get; set; }
        public string Type { get; set; }
    }
}
