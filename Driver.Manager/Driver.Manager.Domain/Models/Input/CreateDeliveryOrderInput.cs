using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Manager.Domain.Models.Input
{
    public class CreateDeliveryOrderInput
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    public class CreateDeliveryOrderInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid UserId { get; set; }
    }
}
