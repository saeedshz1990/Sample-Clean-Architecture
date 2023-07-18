namespace Sample_Clean_Architecture.Domain.Entities.Users
{
    public class UserInRole
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int Users_Id { get; set; }
        public virtual Role Role { get; set; }
        public long RoleId { get; set; }
    }
}
