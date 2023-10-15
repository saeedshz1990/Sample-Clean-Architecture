using Demo.Application.Interfaces.Contexts;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix
{
    public interface IGetSuffixPrefixService
    {
        ResultDto<SuffixPrefixDto> Execute(int Company_Id);
    }
    public class GetSuffixPrefixService : IGetSuffixPrefixService
    {
        private readonly IDatabaseContext _context;
        public GetSuffixPrefixService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<SuffixPrefixDto> Execute(int Company_Id)
        {


            List<SuffixPrefix_Dto> suffixPrefixLst = _context.sp_SuffixPrefix_Get(Company_Id);



            return new ResultDto<SuffixPrefixDto>()
            {
                Data = new SuffixPrefixDto()
                {
                    SuffixPrefixLst = suffixPrefixLst,
                    Company_Id = Company_Id
                },
                IsSuccess = true,
                Message = "",
            };
        }
    }

    public class SuffixPrefixDto
    {
        public int Company_Id { get; set; }
        public List<SuffixPrefix_Dto> SuffixPrefixLst { get; set; }
    }


    public class SuffixPrefix_Dto
    {
        public int SuffixPrefix_Id { get; set; }
        public int VoucherType_Id { get; set; }
        public int No { get; set; }
        public string voucherType_Name { get; set; }
        public int Company_Id { get; set; }
        public DateTime SuffixPrefix_FromDate { get; set; }
        public DateTime SuffixPrefix_ToDate { get; set; }
        public int SuffixPrefix_StartIndex { get; set; }
        public string SuffixPrefix_Prefix { get; set; }
        public string SuffixPrefix_Suffix { get; set; }
        public byte SuffixPrefix_widthOfNumericalPart { get; set; }
        public string SuffixPrefix_PrefillWithCharacter { get; set; }
        public byte SuffixPrefix_Status { get; set; }

        public string Narration { get; set; }



    }

}
