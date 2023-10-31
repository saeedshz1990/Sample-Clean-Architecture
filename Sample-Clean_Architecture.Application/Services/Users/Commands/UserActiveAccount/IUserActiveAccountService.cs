using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

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
