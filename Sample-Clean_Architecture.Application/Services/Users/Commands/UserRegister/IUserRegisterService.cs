using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Users.Commands.UserRegister
{
    public interface IUserRegisterService
    {
        ResultDto<ResultUserRegisterDto> Execute(RequestUserRegisterDto request);
    }
    public class UserRegisterService : IUserRegisterService
    {
        private readonly IDatabaseContext _context;


        public UserRegisterService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultUserRegisterDto> Execute(RequestUserRegisterDto request)
        {
            try
            {
                InsertAccountDto insertAccountDto = _context.Sp_Account_Insert(request.Email, request.Name, request.Password);
                if (insertAccountDto.Error == 0)
                {
                    return new ResultDto<ResultUserRegisterDto>
                    {
                        Data = new ResultUserRegisterDto() { Accounts_Id = insertAccountDto.Accounts_Id, Users_Id = insertAccountDto.Users_Id },
                        IsSuccess = true,
                        Message = AppMessages.USER_REGISTER_SUCCESS
                    };
                }
                else //if (insertAccountDto.Error == 1)
                {
                    return new ResultDto<ResultUserRegisterDto>
                    {

                        IsSuccess = true,
                        Message = AppMessages.USER_EXISTS,
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResultDto<ResultUserRegisterDto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }

    public class ResultUserRegisterDto
    {
        public int Users_Id { get; set; }
        public long Accounts_Id { get; set; }

    }
    public class RequestUserRegisterDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }

}
