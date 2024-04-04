using Driver.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Domain.Models.Output
{
    public class GetAvailableDriversOutput : BaseOutput
    {
        public GetAvailableDriversOutput()
        {
            Rents = new List<GetAvailableDriverItem>();
        }
        public List<GetAvailableDriverItem> Rents { get; set; }

    }

    public class GetAvailableDriverItem
    {
        public Guid RentID { get; set; }
    }
}
