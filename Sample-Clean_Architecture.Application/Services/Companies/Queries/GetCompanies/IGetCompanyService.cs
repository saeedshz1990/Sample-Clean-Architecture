using Demo.Application.Interfaces.Contexts;
using Demo.Common.Dtos;
using Demo.Domain.Entities.Companies;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanies
{
    public interface IGetCompanyService
    {
        ResultDto<List<CompaniesList_Dto>> Execute(int Accounts_Id, int Page = 1, int PageSize = 20);
    }

    public class GetCompanyService : IGetCompanyService
    {
        private readonly IDatabaseContext _context;
        public GetCompanyService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CompaniesList_Dto>> Execute(int Accounts_Id, int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;

            List<CompaniesList_Dto> companies = _context.Sp_Company_List(Accounts_Id);



            return new ResultDto<List<CompaniesList_Dto>>()
            {
                Data = companies,
                IsSuccess = true,
                Message = "",
            };
        }
    }


    public class CompaniesList_Dto
    {
        public int Company_Id { get; set; }
        public string Company_BusinessName { get; set; }
        public string Company_AliasName { get; set; }
        public byte Company_TransactionType { get; set; }


    }
}
