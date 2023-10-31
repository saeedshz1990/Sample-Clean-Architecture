using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Queries.GetExchangeRate;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.ExchangeRate.Commands.AddNewExchangeRate
{
    public interface IAddNewExchangeRateService
    {
        ResultDto Execute(ExchangeRateInfoById_Dto request);
    }

    public class AddNewExchangeRateService : IAddNewExchangeRateService
    {
        private readonly IDatabaseContext _context;


        public AddNewExchangeRateService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(ExchangeRateInfoById_Dto request)
        {
            try
            {
                if (_context.sp_ExchangeRate_Insert(request) == 1)
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
