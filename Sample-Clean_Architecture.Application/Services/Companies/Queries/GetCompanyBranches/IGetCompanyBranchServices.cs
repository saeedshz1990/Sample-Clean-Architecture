using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyBranches
{
    public interface IGetCompanyBranchServices
    {
        ResultDto<CompanyBranchDto> Execute(int Company_Id);
    }
    public class GetCompanyBranchServices : IGetCompanyBranchServices
    {
        private readonly IDatabaseContext _context;
        public GetCompanyBranchServices(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<CompanyBranchDto> Execute(int Company_Id)
        {
            List<CompanyBranch_Dto> companyBrancheLst = _context.sp_CompanyBranch_List(Company_Id);
            return new ResultDto<CompanyBranchDto>()
            {
                Data = new CompanyBranchDto()
                {
                    CompanyBranches = companyBrancheLst,
                    Company_Id = Company_Id
                },
                IsSuccess = true,
                Message = "",
            };
        }
    }

    public class CompanyBranchDto
    {
        public int Company_Id { get; set; }
        public List<CompanyBranch_Dto> CompanyBranches { get; set; }
    }

    public class CompanyBranch_Dto
    {
        public int CompanyBranch_Id { get; set; }
        public int Company_Id { get; set; }
        public string CompanyBranch_Title { get; set; }

    }

}
