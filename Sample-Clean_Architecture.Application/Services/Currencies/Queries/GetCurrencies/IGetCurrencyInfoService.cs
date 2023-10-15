using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Currencies.Queries.GetCurrencies
{
    public interface IGetCurrencyInfoService
    {
        ResultDto<Currency_Dto> Execute(long currency_id);
    }

    public class GetCurrencyInfoService : IGetCurrencyInfoService
    {
        private readonly IDatabaseContext _context;


        public GetCurrencyInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<Currency_Dto> Execute(long currency_id)
        {
            try
            {
                Currency_Dto currency = _context.sp_CurrencyCompany_GetById(currency_id);
                if (currency != null)
                {
                    return new ResultDto<Currency_Dto>
                    {
                        Data = currency,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<Currency_Dto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<Currency_Dto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }
        }
    }

    public class Currency_Dto
    {
        public long Currency_Id { get; set; }
        public int Company_Id { get; set; }
        public string Currency_Name { get; set; }
        public string Currency_Symbol { get; set; }
        public string Currency_Subunit { get; set; }

    }
}
