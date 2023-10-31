using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyUsers;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyUser
{
    public interface IAddNewCompanyUserServices
    {
        ResultDto<ResultCompanyUserDto> Execute(CompanyUserDto request);
    }
    public class AddNewCompanyUserServices : IAddNewCompanyUserServices
    {
        private readonly IDatabaseContext _context;


        public AddNewCompanyUserServices(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultCompanyUserDto> Execute(CompanyUserDto request)
        {
            try
            {
                ResultCompanyUserDto resultCompanyUserDto = _context.Sp_CompanyUsers_Insert(request);
                if (resultCompanyUserDto.StatusOpr != 5)
                {
                    return new ResultDto<ResultCompanyUserDto>
                    {
                        Data = resultCompanyUserDto,
                        IsSuccess = true,
                        Message = AppMessages.USER_REGISTER_SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<ResultCompanyUserDto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResultDto<ResultCompanyUserDto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }

    public class ResultCompanyUserDto
    {
        public int Users_Id { get; set; }
        public int CompanyUsers_Id { get; set; }
        public byte StatusOpr { get; set; }

    }
}
