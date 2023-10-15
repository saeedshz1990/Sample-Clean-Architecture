using Demo.Application.Interfaces.Contexts;
using Demo.Common.Dtos;
using System.Collections.Generic;

namespace Sample_Clean_Architecture.Application.Services.Beneficiary.Queries.GetBenefeciaries
{
    public interface IGetBenefeciaryService
    {
        ResultDto<List<BenefeciaryList_Dto>> Execute(int Company_Id);
    }

    public class GetBenefeciaryService : IGetBenefeciaryService
    {
        private readonly IDatabaseContext _context;
        public GetBenefeciaryService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<BenefeciaryList_Dto>> Execute(int Company_Id)
        {
            List<BenefeciaryList_Dto> benefeciaries = _context.sp_Beneficiary_List(Company_Id);
            return new ResultDto<List<BenefeciaryList_Dto>>()
            {
                Data = benefeciaries,
                IsSuccess = true,
                Message = "",
            };
        }
    }

    public class BenefeciaryList_Dto
    {
        public int Beneficiary_Id { get; set; }
        public string Beneficiary_Name { get; set; }
        public string Beneficiary_Mobile { get; set; }
        public string Beneficiary_Passport { get; set; }
        public string Beneficiary_IdNumber { get; set; }
    }
}
