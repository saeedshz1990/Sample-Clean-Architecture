using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using Sample_Clean_Architecture.Application.Services.Currencies.Queries.GetCurrencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
