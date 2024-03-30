using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Domain.Models.Input
{
    public class DeleteVehicleInput
    {
        public Guid VehicleId { get; set; }
    }

    public class DeleteVehicleInputModel
    {
        public Guid VehicleId { get; set; }
        public Guid UserId { get; set; }
    }
}
