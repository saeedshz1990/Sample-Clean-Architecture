namespace Sample_Clean_Architecture.Domain.Entities.Users
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UserInRole> UserInRoles { get; set; }
    }
}
