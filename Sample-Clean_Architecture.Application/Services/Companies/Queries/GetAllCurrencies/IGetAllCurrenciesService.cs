using DumiSoft.Application.Interfaces.Contexts;
using DumiSoft.Common;
using DumiSoft.Common.Dto;
using DumiSoft.Domain.Entities.Companies;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Companies.Queries.GetAllCurrencies
{
    public interface IGetAllCurrenciesService
    {
        ResultDto<List<AllCurrenciesDto>> Execute();
    }


    public class GetAllCurrenciesService : IGetAllCurrenciesService
    {
        private readonly IDatabaseContext _context;


        public GetAllCurrenciesService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<AllCurrenciesDto>> Execute()
        {

            List<Currency> currencies = _context.Currencies_GetAll();
            List<AllCurrenciesDto> currenciesLst = new List<AllCurrenciesDto>();
            foreach (Currency currency in currencies)
            {
                currenciesLst.Add(new AllCurrenciesDto()
                {
                    Currency_Id = currency.Currency_Id,
                    Currency_Name = currency.Currency_Name,
                    Currency_CountryName = currency.Currency_CountryName,
                });
            }
            return new ResultDto<List<AllCurrenciesDto>>
            {
                Data = currenciesLst,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };


        }
    }

    public class AllCurrenciesDto
    {
        public int Currency_Id { get; set; }
        public string Currency_Name { get; set; }
        public string Currency_CountryName { get; set; }
    }



}
