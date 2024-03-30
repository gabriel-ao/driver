namespace Driver.Domain.Models.Base
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
