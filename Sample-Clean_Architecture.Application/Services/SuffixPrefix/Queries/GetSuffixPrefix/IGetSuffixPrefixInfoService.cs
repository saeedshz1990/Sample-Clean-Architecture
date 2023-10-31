using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix
{
    public interface IGetSuffixPrefixInfoService
    {
        public ResultDto<SuffixPrefix_Dto> Execute(int suffixPrefix_Id);
    }
    public class GetSuffixPrefixInfoService : IGetSuffixPrefixInfoService
    {
        private readonly IDatabaseContext _context;


        public GetSuffixPrefixInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<SuffixPrefix_Dto> Execute(int suffixPrefix_Id)
        {
            try
            {
                SuffixPrefix_Dto suffixPrefix_Dto = _context.sp_SuffixPrefix_GetById(suffixPrefix_Id);
                if (suffixPrefix_Dto != null)
                {
                    return new ResultDto<SuffixPrefix_Dto>
                    {
                        Data = suffixPrefix_Dto,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<SuffixPrefix_Dto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<SuffixPrefix_Dto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
}
