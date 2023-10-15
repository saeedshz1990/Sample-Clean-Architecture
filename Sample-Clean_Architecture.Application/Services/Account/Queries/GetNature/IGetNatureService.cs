using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Account.Queries.GetNature
{
    public interface IGetNatureService
    {
        ResultDto<int> Execute(int accountGroup_Id);
    }
    public class GetNatureService : IGetNatureService
    {
        private readonly IDatabaseContext _context;

        public GetNatureService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<int> Execute(int accountGroup_Id)
        {
            int result = _context.sp_AccountGroup_GetNature(accountGroup_Id);

            return new ResultDto<int>()
            {
                Data = result,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}
