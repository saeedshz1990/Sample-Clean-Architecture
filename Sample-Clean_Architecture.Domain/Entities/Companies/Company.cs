namespace Sample_Clean_Architecture.Domain.Entities.Companies
{
    public class Company
    {
        public int Id { get; set; }
        public string BusinessName { get; set; } = string.Empty;
        public string AliasName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string WebAddress { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public byte DateFormats_Id { get; set; }
        public int Accounts_Id { get; set; }
        public byte Currency_Id { get; set; } 
    }
}
