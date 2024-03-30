﻿using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;

namespace Driver.Domain.Interfaces.Services
{
    public interface IDriverService
    {
        CreateDriverOutput CreateDriver(CreateDriverInput input);
    }
}