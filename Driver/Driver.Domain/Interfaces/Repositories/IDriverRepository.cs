﻿using Driver.Domain.Models.Base;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;

namespace Driver.Domain.Interfaces.Repositories
{
    public interface IDriverRepository
    {
        CreateDriverOutputModel CreateDriver(CreateDriverInput input);
        CreateRentOutput CreateRent(CreateRentInputModel input);
        UpdateRentOutput UpdateRent(UpdateRentInputModel input);
        BaseOutput UpdateCNH(string urlImage, Guid userId);
        BaseOutput AcceptDeliveryOrder(AcceptDeliveryOrderInputModel input);
        BaseOutput FinishDeliveryOrder(FinishDeliveryOrderInputModel input);
        CnhTypesOutput CnhTypes();
        PlansOutput Plans();
        NotificartionDetailsOutput NotificartionDetails(Guid orderId, Guid userId);
        GetNotificationsOutput GetNotifications(Guid userId);
    }
}
