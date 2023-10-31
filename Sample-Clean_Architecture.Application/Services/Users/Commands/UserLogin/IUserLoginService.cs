using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Common.Commands.UserProfile;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetMenuItem;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Users.Commands.UserLogin
{
    public interface IUserLoginService
    {
        ResultDto<ResultUserloginDto> Execute(string username, string password);
    }

    public class UserLoginService : IUserLoginService
    {
        private readonly IDatabaseContext _context;
        public UserLoginService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultUserloginDto> Execute(string username, string password)
        {

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return new ResultDto<ResultUserloginDto>()
                {
                    Data = new ResultUserloginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = AppMessages.UN_PASSREQ,
                };
            }
            // User user=  _context.User_Get(Username);
            byte errorType;

            password = Cryptography.Encrypt(password, "DumiSoft.Com");
            ResultUserloginDto resultUserloginDto = _context.Sp_Users_Login(username, password, out errorType);

            switch (errorType)
            {
                case 0:
                    /* List<string> roles = new List<string>();
                     if (userInformations.UserInRoles != null)
                     {
                         foreach (var item in userInformations.UserInRoles)
                         {
                             roles.Add(item.Role.Name);
                         }
                     }*/
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = resultUserloginDto /*= new ResultUserloginDto()
                        {
                            Roles = roles,
                            Users_Id = userInformations.Users_Id,
                            Accounts_Id = userInformations.Accounts_Id
                            //And Other Info Fill
                        }*/,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };

                case 1:
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto()
                        {

                        },
                        IsSuccess = false,
                        Message = AppMessages.UN_PASS_INVALID,
                    };

                case 2:
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto()
                        {

                        },
                        IsSuccess = false,
                        Message = AppMessages.USER_NOT_ACTIVATED,
                    };
                case 3:
                    return new ResultDto<ResultUserloginDto>()
                    {
                        Data = new ResultUserloginDto()
                        {

                        },
                        IsSuccess = false,
                        Message = AppMessages.UN_PASS_INVALID,
                    };


            }

            /* if (user == null)
             {
                 return new ResultDto<ResultUserloginDto>()
                 {
                     Data = new ResultUserloginDto()
                     {

                     },
                     IsSuccess = false,
                     Message = AppMessages.UN_PASS_INVALID,
                 };
             }*/
            /*   var passwordHasher = new PasswordHasher();
               bool resultVerifyPassword = passwordHasher.VerifyPassword(user.Users_Password, Password);
               if (resultVerifyPassword == false)
               {
                   return new ResultDto<ResultUserloginDto>()
                   {
                       Data = new ResultUserloginDto()
                       {

                       },
                       IsSuccess = false,
                       Message = AppMessages.UN_PASS_INVALID,
                   };
               }*/

            return new ResultDto<ResultUserloginDto>()
            {
                Data = new ResultUserloginDto()
                {

                },
                IsSuccess = false,
                Message = AppMessages.UNKNOWN,
            };
        }
    }
    public class ResultUserloginDto
    {
        public int Users_Id { get; set; }
        public int Accounts_Id { get; set; }
        public int Company_Id { get; set; }
        public int CompanyUsers_Id { get; set; }
        public List<UserBusinessDto> UserInBusiness { get; set; }
        public List<MenuItemDto> Menus { get; set; }
        public byte Company_TransactionType { get; set; }
        public byte DateFormats_Id { get; set; }

        public string DateFormats_Description { get; set; }

        public bool IsCurrentDate { get; set; }
        public DateTime WorkDay { get; set; }
        public byte Company_DateSeperator { get; set; }
        public DateTime FinancialCycle_FromDate { get; set; }
        public DateTime FinancialCycle_ToDate { get; set; }
        public List<string> Roles { get; set; }
        public UserProfileDto UserProfile { get; set; }
        public ResultUserloginDto()
        {
            UserInBusiness = new List<UserBusinessDto>();
            Menus = new List<MenuItemDto>();
            Roles = new List<string>();
        }
    }

    public class UserBusinessDto
    {
        public int CompanyUsers_Id { get; set; }
        public string Company_BusinessName { get; set; }
    }
}
