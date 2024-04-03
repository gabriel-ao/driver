namespace Driver.Notification.Models.Input
{
    public class PushNotificationInput
    {
        public PushNotificationInput()
        {
            Rents = new List<RentItem>();
        }
        public List<RentItem> Rents { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid OrderId { get; set; }

    }

    public class RentItem
    {
        public Guid RentID { get; set; }
    }
}
