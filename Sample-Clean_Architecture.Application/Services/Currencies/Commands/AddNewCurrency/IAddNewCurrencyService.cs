using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Currencies.Queries.GetCurrencies;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Currencies.Commands.AddNewCurrency
{
    public interface IAddNewCurrencyService
    {
        ResultDto Execute(Currency_Dto request);
    }

    public class AddNewCurrencyService : IAddNewCurrencyService
    {
        private readonly IDatabaseContext _context;


        public AddNewCurrencyService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(Currency_Dto request)
        {
            try
            {
                if (_context.sp_CurrencyCompany_Insert(request) == 1)
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
