using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{
    public class SaveDocumentImageOutput : BaseOutput
    {

    }

    public class SaveDocumentImageOutputModel : BaseOutput
    {
        public string urlImage { get; set; }
    }
}
