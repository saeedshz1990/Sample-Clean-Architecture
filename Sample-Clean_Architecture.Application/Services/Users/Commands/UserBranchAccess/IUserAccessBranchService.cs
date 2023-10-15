using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Users.Commands.UserBranchAccess
{
    public interface IUserAccessBranchService
    {
        ResultDto Execute(int Id, string CompanyUsers_IdStr, byte kind);
    }
    public class UserAccessBranchService : IUserAccessBranchService
    {
        private readonly IDatabaseContext _context;


        public UserAccessBranchService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(int Id, string CompanyUsers_IdStr, byte kind)
        {
            try
            {
                if (_context.sp_UsersAccess_Insert(Id, CompanyUsers_IdStr, kind) == 1)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
}
