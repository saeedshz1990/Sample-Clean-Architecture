using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyFinancialCycle
{
    public interface IGetCompanyFinancialCycleInfoService
    {
        public ResultDto<CompanyFinancialCycle_Dto> Execute(int financialCycle_Id);
    }
    public class GetCompanyFinancialCycleInfoService : IGetCompanyFinancialCycleInfoService
    {
        private readonly IDatabaseContext _context;


        public GetCompanyFinancialCycleInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<CompanyFinancialCycle_Dto> Execute(int financialCycle_Id)
        {
            try
            {
                CompanyFinancialCycle_Dto companyFinancialCycle_Dto = _context.Sp_CompanyFinancialCycle_Get(financialCycle_Id);
                if (companyFinancialCycle_Dto != null)
                {
                    return new ResultDto<CompanyFinancialCycle_Dto>
                    {
                        Data = companyFinancialCycle_Dto,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<CompanyFinancialCycle_Dto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<CompanyFinancialCycle_Dto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }

}
