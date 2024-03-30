﻿using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;

namespace Driver.Domain.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        BaseOutput CreateLog(CreateLogInput input);
        BaseOutput UpdateVehicle(UpdateVehicleInputModel input);
    }
}
