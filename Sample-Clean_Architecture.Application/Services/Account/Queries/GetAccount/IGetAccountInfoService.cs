using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountLedger;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccount
{
    public interface IGetAccountInfoService
    {
        ResultDto<AccountLedgerDto> Execute(int Company_Id, int AccountLeger_Id);
    }

    public class GetAccountInfoService : IGetAccountInfoService
    {
        private readonly IDatabaseContext _context;

        public GetAccountInfoService(IDatabaseContext context)
        {
            _context = context;

        }

        public ResultDto<AccountLedgerDto> Execute(int Company_Id, int AccountLeger_Id)
        {
            var account_ledger = _context.sp_AccountLegder_GetById(Company_Id, AccountLeger_Id);

            return new ResultDto<AccountLedgerDto>()
            {
                Data = account_ledger,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}