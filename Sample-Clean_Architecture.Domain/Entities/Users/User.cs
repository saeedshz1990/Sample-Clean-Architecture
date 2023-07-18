namespace Sample_Clean_Architecture.Domain.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte Status { get; set; }
        public ICollection<UserInRole> UserInRoles { get; set; }
    }
}
