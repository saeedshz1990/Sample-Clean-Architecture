using Sample_Clean_Architecture.Application.Services.Users.Commands.UserLogin;

namespace Sample_Clean_Architecture.Web.Utilities
{
    public class ActiveUser : ResultUserloginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
