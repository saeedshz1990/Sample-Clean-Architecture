using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompany;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanies
{
    public interface IGetCompanyInfoService
    {
        ResultDto<CompanyGetDto> Execute(int company_Id);
    }
    public class GetCompanyInfoService : IGetCompanyInfoService
    {
        private readonly IDatabaseContext _context;


        public GetCompanyInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<CompanyGetDto> Execute(int company_Id)
        {
            try
            {
                CompanyGetDto companyGetDto = _context.Sp_Company_Get(company_Id);
                if (companyGetDto != null)
                {
                    return new ResultDto<CompanyGetDto>
                    {
                        Data = companyGetDto,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<CompanyGetDto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<CompanyGetDto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }

    public class CompanyGetDto
    {
        public List<DateFormatDto> DateFormats { get; set; }
        public List<CurrencyDto> Currencies { get; set; }
        public List<CountryDto> Countries { get; set; }
        public List<DefaultLedgerDto> DefaultLedgers { get; set; }
        public RequestCompanyDto RequestCompany { get; set; }
    }
    public class CurrencyDto
    {
        public int Currency_Id { get; set; }
        public string Currency_Name { get; set; }
        public string Currency_Symbol { get; set; }


    }
    public class CountryDto
    {
        public int Country_Id { get; set; }
        public string Country_Name { get; set; }
        public string Currency_Name { get; set; }
        public string Currency_Subunit { get; set; }


    }
    public class DateFormatDto
    {
        public int DateFormats_Id { get; set; }
        public string DateFormats_Description { get; set; }
    }

    public class DefaultLedgerDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
