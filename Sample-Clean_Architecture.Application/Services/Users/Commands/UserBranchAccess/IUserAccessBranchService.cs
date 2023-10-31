using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

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
