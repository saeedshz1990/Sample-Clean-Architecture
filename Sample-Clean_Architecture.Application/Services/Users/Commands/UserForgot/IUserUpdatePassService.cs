using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Users.Commands.UserForgot
{
    public interface IUserUpdatePassService
    {
        ResultDto Execute(int users_Id, string password);
    }
    public class UserUpdatePassService : IUserUpdatePassService
    {
        private readonly IDatabaseContext _context;
        public UserUpdatePassService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int users_Id, string password)
        {

            if (_context.Sp_Users_UpdatePassword(users_Id, password) == 1)

            {
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = AppMessages.SUCCESS
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR
                };
            }
        }
    }
}
