using Sample_Clean_Architecture.Application.Services.Users.Commands.UserLogin;

namespace Sample_Clean_Architecture.Web.Models.Companies
{
    public class UserCompaniesModel
    {
        public int CompanyUsers_Id { get; set; }
        public List<UserBusinessDto> UserBusiness { get; set; }
    }
}
