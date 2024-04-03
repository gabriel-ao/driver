

using Driver.Notification;

namespace ConsumerApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"Consume rabbit == {DateTime.Now}");
                var consumerService = new MessageConsumerService();
                consumerService.ConsumeAvailableOrders();

                Console.WriteLine($"finish rabbit == {DateTime.Now}");
                Thread.Sleep(60000); // Wait 1 minute before checking again

            }
        }
    }
}
