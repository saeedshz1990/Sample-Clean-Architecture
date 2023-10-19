namespace Sample_Clean_Architecture.Web.Models.UserAccess
{
    public class UserAccessModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ParentId { get; set; }
        public int CompanyUsers_MenuId { get; set; }
    }
}
