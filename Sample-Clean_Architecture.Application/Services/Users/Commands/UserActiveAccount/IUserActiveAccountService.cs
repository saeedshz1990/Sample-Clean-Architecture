using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Users.Commands.UserActiveAccount
{
    public interface IUserActiveAccountService
    {
        ResultDto<int> Execute(RequestUserActiveAccountDto request);
    }
    public class UserActiveAccountService : IUserActiveAccountService
    {
        private readonly IDatabaseContext _context;


        public UserActiveAccountService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<int> Execute(RequestUserActiveAccountDto request)
        {
            try
            {
                int result = _context.Sp_Account_Activate(request.UserId, request.AccountId);

                switch (result)
                {
                    case 1:
                        return new ResultDto<int>
                        {
                            Data = result,
                            IsSuccess = true,
                            Message = AppMessages.ACCOUNT_ACTIVE_SUCCESS
                        };

                    case 0:

                        return new ResultDto<int>
                        {
                            Data = result,
                            IsSuccess = true,
                            Message = AppMessages.USER_ACTIVATED,
                        };

                    case 2:
                        return new ResultDto<int>
                        {
                            Data = result,
                            IsSuccess = true,
                            Message = AppMessages.INVALID_LINK,

                        };

                    default:
                        return new ResultDto<int>
                        {
                            Data = result,
                            IsSuccess = false,
                            Message = AppMessages.UNKNOWN,

                        };
                }

            }
            catch (Exception ex)
            {
                return new ResultDto<int>
                {
                    Data = -1,
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
    public class RequestUserActiveAccountDto
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }


    }
}
