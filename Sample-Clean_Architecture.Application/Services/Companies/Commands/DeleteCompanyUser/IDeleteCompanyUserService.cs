using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Companies.Commands.DeleteCompanyUser
{
    public interface IDeleteCompanyUserService
    {
        ResultDto Execute(int companyUsers_Id);
    }
    public class DeleteCompanyUserService : IDeleteCompanyUserService
    {
        private readonly IDatabaseContext _context;


        public DeleteCompanyUserService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(int companyUsers_Id)
        {
            try
            {

                bool error = false;
                if (_context.Sp_CompanyUsers_DeletePending(companyUsers_Id, out error) == 2)
                {
                    if (error == false)
                    {
                        return new ResultDto
                        {
                            IsSuccess = true,
                            Message = AppMessages.SUCCESS,
                        };
                    }
                    else
                    {
                        return new ResultDto
                        {
                            IsSuccess = true,
                            Message = AppMessages.UNABLE_DELETE,
                        };

                    }
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
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
