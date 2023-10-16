using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher
{
    public interface ILoadJournalVoucherService
    {
        ResultDto<JournalVoucherLoadDto> Execute(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);
    }
    public class LoadJournalVoucherService : ILoadJournalVoucherService
    {
        private readonly IDatabaseContext _context;

        public LoadJournalVoucherService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<JournalVoucherLoadDto> Execute(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate)
        {
            var data = _context.sp_JournalVoucher_Load(Company_Id, Users_Id, CompanyUsers_Id, CurrentDate, VoucherDate);

            return new ResultDto<JournalVoucherLoadDto>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class JournalVoucherLoadDto
    {
        public string VoucherNumber { get; set; }
        public DateTime VoucherDate { get; set; }
        public bool ProjectActive { get; set; }
        public bool CostCenterActive { get; set; }
        public InFormAccess InFormAccess { get; set; }
        public List<CurrencyCompany> CurrencyCompanyList { get; set; }
        public List<CompanyBranch> CompanyBranchList { get; set; }
        public List<Project> ProjectList { get; set; }
        public List<AccountLedger> AccountLedgerList { get; set; }
        public List<CostCenter> CostCenterList { get; set; }
        public List<VoucherType> VoucherTypeList { get; set; }

        public JournalVoucherLoadDto()
        {
            InFormAccess = new InFormAccess();
            CurrencyCompanyList = new List<CurrencyCompany>();
            CompanyBranchList = new List<CompanyBranch>();
            ProjectList = new List<Project>();
            AccountLedgerList = new List<AccountLedger>();
            CostCenterList = new List<CostCenter>();
            VoucherTypeList = new List<VoucherType>();
        }
    }
    public class CurrencyCompany
    {
        public int Currency_Id { get; set; }
        public string Currency_Name { get; set; }
        public float Rate { get; set; }
        public long ExchangeRate_Id { get; set; }
    }
    public class CompanyBranch
    {
        public int CompanyBranch_Id { get; set; }
        public string CompanyBranch_Title { get; set; }
    }
    public class Project
    {
        public int Projects_Id { get; set; }
        public string Projects_Name { get; set; }
    }

    public class AccountLedger
    {
        public int Ledger_Id { get; set; }
        public string Ledger_Name { get; set; }
        public int Currency_Id { get; set; }
        public bool BillbyBill { get; set; }
    }

    public class CostCenter
    {
        public int CostCenter_Id { get; set; }
        public string CostCenter_Name { get; set; }
    }
    public class VoucherType
    {
        public int VoucherLabel_Id { get; set; }
        public string VoucherLabel_Title { get; set; }
    }

}
