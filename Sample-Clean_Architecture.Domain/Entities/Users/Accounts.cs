namespace Sample_Clean_Architecture.Domain.Entities.Users
{
    public class Accounts
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public byte Status { get; set; }
        public int Users_Id { get; set; }
    }
}
