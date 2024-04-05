namespace Driver.Auth.Domain.Models.Input
{
    public class CreateLogInput
    {
        public string MethodName { get; set; }
        public string Message { get; set; }
        public string StackMessage { get; set; }
        public string Type { get; set; }
    }
}
