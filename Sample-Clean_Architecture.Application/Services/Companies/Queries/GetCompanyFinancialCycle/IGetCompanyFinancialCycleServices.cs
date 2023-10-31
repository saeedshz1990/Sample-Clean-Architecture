using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyFinancialCycle
{
    public interface IGetCompanyFinancialCycleServices
    {
        ResultDto<CompanyFinancialCycleDto> Execute(int Company_Id);
    }
    public class GetCompanyFinancialCycleServices : IGetCompanyFinancialCycleServices
    {
        private readonly IDatabaseContext _context;
        public GetCompanyFinancialCycleServices(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<CompanyFinancialCycleDto> Execute(int Company_Id)
        {
            List<CompanyFinancialCycle_Dto> companyFinancialCycleLst = _context.Sp_CompanyFinancialCycle_List(Company_Id);

            return new ResultDto<CompanyFinancialCycleDto>()
            {
                Data = new CompanyFinancialCycleDto()
                {
                    CompanyFinancialCycles = companyFinancialCycleLst,
                    Company_Id = Company_Id
                },
                IsSuccess = true,
                Message = "",
            };
        }
    }

    public class CompanyFinancialCycleDto
    {
        public int Company_Id { get; set; }
        public List<CompanyFinancialCycle_Dto> CompanyFinancialCycles { get; set; }
    }

    public class CompanyFinancialCycle_Dto
    {
        public int FinancialCycle_Id { get; set; }
        public int Company_Id { get; set; }
        public string FinancialCycle_Title { get; set; }
        public DateTime FinancialCycle_FromDate { get; set; }
        public DateTime FinancialCycle_ToDate { get; set; }
        public bool FinancialCycle_isActive { get; set; }
    }
}
