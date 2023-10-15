using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
