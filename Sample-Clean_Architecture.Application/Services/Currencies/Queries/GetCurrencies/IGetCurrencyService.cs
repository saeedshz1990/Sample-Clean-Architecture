using Demo.Application.Interfaces.Contexts;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Currencies.Queries.GetCurrencies
{
    public interface IGetCurrencyService
    {
        ResultDto<List<CurrencyList_Dto>> Execute(int Company_Id);
    }

    public class GetCurrencyService : IGetCurrencyService
    {
        private readonly IDatabaseContext _context;
        public GetCurrencyService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CurrencyList_Dto>> Execute(int Company_Id)
        {
            List<CurrencyList_Dto> currencies = _context.sp_CurrencyCompany_List(Company_Id);
            return new ResultDto<List<CurrencyList_Dto>>()
            {
                Data = currencies,
                IsSuccess = true,
                Message = "",
            };
        }
    }

    public class CurrencyList_Dto
    {
        public long Currency_Id { get; set; }
        public string Currency_Name { get; set; }
        public string Currency_Symbol { get; set; }
    }
}
