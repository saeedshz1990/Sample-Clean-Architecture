using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.SuffixPrefix.Commands.AddNewSuffixPrefix
{
    public interface IAddNewSuffixPrefixService
    {
        ResultDto Execute(SuffixPrefix_Dto request);
    }
    public class AddNewSuffixPrefixService : IAddNewSuffixPrefixService
    {
        private readonly IDatabaseContext _context;


        public AddNewSuffixPrefixService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(SuffixPrefix_Dto request)
        {
            try
            {


                if (_context.sp_SuffixPrefix_Insert(request) == -1)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };

                }
            }
            catch (Exception ex)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
}
