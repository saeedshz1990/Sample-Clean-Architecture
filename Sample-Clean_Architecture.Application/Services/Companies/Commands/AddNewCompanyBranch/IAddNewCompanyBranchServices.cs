using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyBranches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyBranch
{
    public interface IAddNewCompanyBranchServices
    {
        ResultDto Execute(CompanyBranch_Dto request);
    }
    public class AddNewCompanyBranchServices : IAddNewCompanyBranchServices
    {
        private readonly IDatabaseContext _context;


        public AddNewCompanyBranchServices(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(CompanyBranch_Dto request)
        {
            try
            {

                if (_context.sp_CompanyBranch_Insert(request) == -1)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };

                }
            }
            catch (Exception ex)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
}
