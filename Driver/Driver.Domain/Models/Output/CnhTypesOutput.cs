using Driver.Domain.Models.Base;

namespace Driver.Domain.Models.Output
{
    public class CnhTypesOutput : BaseOutput
    {
        public CnhTypesOutput()
        {
            CnhTypes = new List<CnhTypesItem>();
        }
        public List<CnhTypesItem> CnhTypes { get; set; }
    }

    public class CnhTypesItem
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
    }
}
