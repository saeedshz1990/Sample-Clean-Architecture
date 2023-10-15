using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountGroup;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountLedger
{
    public interface IAddNewAccountLedgerService
    {
        ResultDto Execute(AccountLedgerDto accountLedgerDto);
    }
    public class AddNewAccountLedgerService : IAddNewAccountLedgerService
    {
        private readonly IDatabaseContext _context;

        public AddNewAccountLedgerService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(AccountLedgerDto accountLedgerDto)
        {
            try
            {
                if (_context.sp_AccountLegder_Insert(accountLedgerDto) == 2)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
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
    public class AccountLedgerDto
    {
        public int Ledger_Id { get; set; }
        public int Company_Id { get; set; }
        public string Ledger_Name { get; set; }
        public int AccountGroup_Id { get; set; }
        public int Ledger_Code { get; set; }
        public long Currency_Id { get; set; }
        public bool Ledger_BillByBill { get; set; }
        public int Ledger_Status { get; set; }
        public string LedgerBank_BankName { get; set; }
        public string LedgerBank_BranchName { get; set; }
        public string LedgerBank_BranchCode { get; set; }
        public string LedgerBank_AccountNumber { get; set; }
        public string LedgerBank_AccountName { get; set; }
        public string LedgerBank_IBAN { get; set; }
        public string LedgerBank_Swift { get; set; }
        public string LedgerBank_HeaderNote { get; set; }
        public string LedgerBank_FooterNote { get; set; }
        public decimal LedgerDetails_CreditLimit { get; set; }
        public int LedgerDetails_CreditPeriod { get; set; }
        public string LedgerDetails_MailingName { get; set; }
        public string LedgerDetails_Branch { get; set; }
        public string LedgerDetails_Email { get; set; }
        public string LedgerDetails_Address { get; set; }
        public string LedgerDetails_ContactPerson { get; set; }
        public string LedgerDetails_Mobile1 { get; set; }
        public string LedgerDetails_Mobile2 { get; set; }
        public string LedgerDetails_Phone { get; set; }
        public string LedgerDetails_Fax { get; set; }
        public string LedgerDetails_Narration { get; set; }
        public string LedgerDetails_BankIBAN { get; set; }
        public string LedgerDetails_BankBranchName { get; set; }
        public string LedgerDetails_BankBranchCode { get; set; }
        public string LedgerDetails_BankSwiftCode { get; set; }
        public string LedgerDetails_BankAccountNumber { get; set; }
        public string LedgerDetails_BankNameOnCheque { get; set; }
        public string LedgerDetails_ShipTo { get; set; }
        public int TermsAndCondition_Id { get; set; }
        public string LedgerDetails_CST { get; set; }
        public string LedgerDetails_TIN { get; set; }
        public string LedgerDetails_VAT { get; set; }
        public string LedgerDetails_PAN { get; set; }

        public List<AccountGroup> AcountGroupList { get; set; }
        public List<TermsAndCondition> TermsAndConditionList { get; set; }

        public List<CurrencyCompany> CurrencyCompanyList { get; set; }

        public AccountLedgerDto()
        {
            AcountGroupList = new List<AccountGroup>();
            TermsAndConditionList = new List<TermsAndCondition>();
            CurrencyCompanyList = new List<CurrencyCompany>();
        }
    }

    public class TermsAndCondition
    {
        public int TermsAndCondition_Id { get; set; }
        public string TermsAndCondition_Name { get; set; }
    }
}
