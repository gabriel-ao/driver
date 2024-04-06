using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Manager.Domain.Models.Base
{
    public class BaseOutput
    {
        public BaseOutput()
        {
            Message = string.Empty;
            Error = false;
        }

        public string Message { get; set; }
        public bool Error { get; set; }
    }
}
