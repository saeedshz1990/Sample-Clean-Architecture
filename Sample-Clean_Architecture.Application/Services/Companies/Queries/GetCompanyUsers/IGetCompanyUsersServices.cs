using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using static Sample_Clean_Architecture.Common.Enums;

namespace Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyUsers
{
    public interface IGetCompanyUsersServices
    {
        ResultDto<CompanyUsersDto> Execute(int Company_Id);
    }
    public class GetCompanyUsersServices : IGetCompanyUsersServices
    {
        private readonly IDatabaseContext _context;
        public GetCompanyUsersServices(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<CompanyUsersDto> Execute(int Company_Id)
        {
            List<CompanyUserDto> companyUsersLst = _context.Sp_CompanyUsers_List(Company_Id);
            if (companyUsersLst != null)
            {
                return new ResultDto<CompanyUsersDto>()
                {
                    Data = new CompanyUsersDto()
                    {
                        CompanyUsers = companyUsersLst,
                        Company_Id = Company_Id
                    },
                    IsSuccess = true,
                    Message = AppMessages.SUCCESS,
                };
            }
            else
                return new ResultDto<CompanyUsersDto>()
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
        }
    }

    public class CompanyUsersDto
    {
        public int Company_Id { get; set; }
        public List<CompanyUserDto> CompanyUsers { get; set; }
    }

    public class CompanyUserDto
    {
        public int Company_Id { get; set; }
        public int CompanyUsers_Id { get; set; }
        public string Users_UserName { get; set; }
        public string Users_Password { get; set; }
        public string Users_Description { get; set; }
        // public byte CompanyUsers_Status { get; set; }
        // public byte Users_Status { get; set; }
        public CompanyUserStatus CompanyUsers_Status { get; set; }
        public UserStatus Users_Status { get; set; }
        public CompanyUserDto()
        {
            Users_Description = string.Empty;
            Users_UserName = string.Empty;
            Users_Password = string.Empty;
        }
    }
}
