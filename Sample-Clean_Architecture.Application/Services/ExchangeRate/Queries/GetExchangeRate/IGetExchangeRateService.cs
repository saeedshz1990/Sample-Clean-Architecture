using Demo.Application.Interfaces.Contexts;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.ExchangeRate.Queries.GetExchangeRate
{
    public interface IGetExchangeRateService
    {
        ResultDto<ExchangeRate_Dto> Execute(int Company_Id, DateTime? curdate);
    }

    public class GetExchangeRateService : IGetExchangeRateService
    {
        private readonly IDatabaseContext _context;
        public GetExchangeRateService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<ExchangeRate_Dto> Execute(int Company_Id, DateTime? curdate)
        {
            ExchangeRate_Dto exchangerate = _context.sp_ExchangeRate_GetList(Company_Id, curdate);

            return new ResultDto<ExchangeRate_Dto>()
            {
                Data = exchangerate == null ? new ExchangeRate_Dto() : exchangerate,
                IsSuccess = true,
                Message = "",
            };
        }
    }


    public class ExchangeRate_Dto
    {
        public string VoucherDate { get; set; }

        public List<ExchangeRateList_Dto> ExchangeRateList_Dto { get; set; }

        public ExchangeRate_Dto()
        {
            ExchangeRateList_Dto = new List<ExchangeRateList_Dto>();
        }

    }
    public class ExchangeRateList_Dto
    {
        public byte No { get; set; }
        public long Currency_Id { get; set; }
        public string Currency_Name { get; set; }
        public decimal Rate { get; set; }
        public long ExchangeRate_Id { get; set; }

        public DateTime ExchangeRate_Date { get; set; }

        public string ExchangeRate_Narration { get; set; }
        public bool ExchangeRate_Used { get; set; }

    }
}
