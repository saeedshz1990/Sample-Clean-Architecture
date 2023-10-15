using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Account.Commands.DeleteAccountLedger
{
    public interface IDeleteAccountLedgerService
    {
        ResultDto Execute(int accountLedger_Id);
    }
    public class DeleteAccountLedgerService : IDeleteAccountLedgerService
    {
        private readonly IDatabaseContext _context;


        public DeleteAccountLedgerService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(int accountLedger_Id)
        {
            try
            {

                bool error = false;
                if (_context.sp_AccountLedger_Delete(accountLedger_Id, out error) == 2)
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
