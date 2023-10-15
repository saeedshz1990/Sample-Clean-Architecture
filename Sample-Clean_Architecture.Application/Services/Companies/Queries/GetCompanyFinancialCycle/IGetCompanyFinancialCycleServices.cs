using Demo.Application.Interfaces.Contexts;
using Demo.Common.Dtos;
using Demo.Domain.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            /*  List<CompanyFinancialCycleList_Dto> companyFinancialCycleLst = new List<CompanyFinancialCycleList_Dto>();
              foreach (CompanyFinancialCycle companyFinancialCycle in companyFinancialCycles)
              {
                  companyFinancialCycleLst.Add(new CompanyFinancialCycleList_Dto()
                  {
                      Company_Id = companyFinancialCycle.Company_Id,
                      FinancialCycle_FromDate = companyFinancialCycle.FinancialCycle_FromDate,
                      FinancialCycle_Id = companyFinancialCycle.FinancialCycle_Id,
                      FinancialCycle_isActive = companyFinancialCycle.FinancialCycle_isActive,
                      FinancialCycle_Title = companyFinancialCycle.FinancialCycle_Title,
                      FinancialCycle_ToDate = companyFinancialCycle.FinancialCycle_ToDate

                  });
              }*/

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
