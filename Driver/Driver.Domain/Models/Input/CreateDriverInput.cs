namespace Driver.Domain.Models.Input
{
    public class CreateDriverInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public Guid CnhID { get; set; }
        public string CnhImage { get; set; }
        public string Password { get; set; }
    }
}
