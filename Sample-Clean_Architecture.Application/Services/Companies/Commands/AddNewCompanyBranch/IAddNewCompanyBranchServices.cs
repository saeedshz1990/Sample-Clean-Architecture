using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyBranches;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

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
