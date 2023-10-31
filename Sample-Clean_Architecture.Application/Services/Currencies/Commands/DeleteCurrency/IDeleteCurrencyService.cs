using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Currencies.Commands.DeleteCurrency
{
    public interface IDeleteCurrencyService
    {
        ResultDto Execute(long currency_id);
    }

    public class DeleteCurrencyService : IDeleteCurrencyService
    {
        private readonly IDatabaseContext _context;


        public DeleteCurrencyService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(long currency_id)
        {
            try
            {

                bool error = false;
                if (_context.sp_CurrencyCompany_Delete(currency_id, out error) == 2)
                {
                    if (error == true)
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
                            IsSuccess = false,
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
