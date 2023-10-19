using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Companies
{
    public class CompanyUserModel
    {
        [Key]
        public int CompanyUsers_Id { get; set; }
        public int Company_Id { get; set; }
        [DisplayName("UserName")]
        //[Required(ErrorMessage = "{0} is required")]
        public string UserName { get; set; }

        [DisplayName("Description")]
        //[Required(ErrorMessage = "{0} is required")]
        public string UserDescription { get; set; }

        [DisplayName("CompanyUserStatus")]
        public string CompanyUserStatus { get; set; }

        [DisplayName("UserStatus")]
        public string UserStatus { get; set; }

        public string Password { get; set; }
    }
}
