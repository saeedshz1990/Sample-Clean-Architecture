using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccount
{
    public interface IGetAccountService
    {
        ResultDto<AccountListDto> Execute(int Company_Id, int AccountGroup_Id, int CompanyUser_Id);
    }

    public class GetAccountService : IGetAccountService
    {
        private readonly IDatabaseContext _context;

        public GetAccountService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<AccountListDto> Execute(int Company_Id, int AccountGroup_Id, int CompanyUser_Id)
        {
            var accounts = _context.sp_AccountLegder_Get(Company_Id, AccountGroup_Id, CompanyUser_Id);

            return new ResultDto<AccountListDto>()
            {
                Data = accounts,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class AccountListDto
    {
        public InFormAccess InFormAccess { get; set; }
        public List<AccountList> AccountList { get; set; }
        public AccountListDto()
        {
            InFormAccess = new InFormAccess();
            AccountList = new List<AccountList>();
        }
    }
    public class AccountList
    {
        public int Account_Node_Id { get; set; }
        public string Account_Node_Name { get; set; } = string.Empty;
        public int Account_AccountGroup_Parent { get; set; }
        public int Account_Is_Group { get; set; }
    }
}