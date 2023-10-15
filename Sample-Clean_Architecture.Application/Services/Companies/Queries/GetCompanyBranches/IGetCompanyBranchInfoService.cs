using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyBranches
{
    public interface IGetCompanyBranchInfoService
    {
        public ResultDto<CompanyBranch_Dto> Execute(int companyBranch_Id);
    }
    public class GetCompanyBranchInfoService : IGetCompanyBranchInfoService
    {
        private readonly IDatabaseContext _context;


        public GetCompanyBranchInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<CompanyBranch_Dto> Execute(int companyBranch_Id)
        {
            try
            {
                CompanyBranch_Dto companyBranch_Dto = _context.Sp_CompanyBranch_Get(companyBranch_Id);
                if (companyBranch_Dto != null)
                {
                    return new ResultDto<CompanyBranch_Dto>
                    {
                        Data = companyBranch_Dto,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<CompanyBranch_Dto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<CompanyBranch_Dto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
}
