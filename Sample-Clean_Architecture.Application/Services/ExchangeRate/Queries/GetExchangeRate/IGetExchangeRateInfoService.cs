using Demo.Application.Interfaces.Contexts;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.ExchangeRate.Queries.GetExchangeRate
{
    public interface IGetExchangeRateInfoService
    {
        ResultDto<ExchangeRateInfo_Dto> Execute(int Company_Id, long exchangerate_id, DateTime dateTime);
    }

    public class GetExchangeRateInfoService : IGetExchangeRateInfoService
    {
        private readonly IDatabaseContext _context;
        public GetExchangeRateInfoService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<ExchangeRateInfo_Dto> Execute(int Company_Id, long exchangerate_id, DateTime dateTime)
        {
            ExchangeRateInfo_Dto exchangerate = _context.sp_ExchangeRate_GetById(Company_Id, exchangerate_id, dateTime);

            return new ResultDto<ExchangeRateInfo_Dto>()
            {
                Data = exchangerate == null ? new ExchangeRateInfo_Dto() : exchangerate,
                IsSuccess = true,
                Message = "",
            };
        }
    }

    public class ExchangeRateInfoById_Dto
    {

        public long ExchangeRate_Id { get; set; }
        public long Currency_Id { get; set; }
        public byte No { get; set; }
        public string Currency_Name { get; set; }

        public decimal Rate { get; set; }
        public DateTime ExchangeRate_Date { get; set; }
        public string ExchangeRate_Narration { get; set; }

    }

    public class ExchangeRateInfo_Dto
    {
        public ExchangeRateInfoById_Dto ExchangeRateInfoById { get; set; }
        public List<ExchangeRateInfoDetail_Dto> ExchangeRateInfoDetail_Dto { get; set; }

        public ExchangeRateInfo_Dto()
        {
            ExchangeRateInfoDetail_Dto = new List<ExchangeRateInfoDetail_Dto>();
            /*ExchangeRate_Id = 0;
            Currency_Id = 0;
            No = 0;
            Currency_Name = "";
            Rate = 0;
            ExchangeRate_Narration = "";*/
        }

    }
    public class ExchangeRateInfoDetail_Dto
    {
        public long Currency_Id { get; set; }
        public string Currency_Name { get; set; }

    }
}

