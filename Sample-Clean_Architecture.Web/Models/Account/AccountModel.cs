using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Account
{
    public class AccountModel
    {
        [Key]
        public int Ledger_Id { get; set; }
        public int Company_Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [MinLength(2, ErrorMessage = "Minimum {1} characters")]
        [MaxLength(150, ErrorMessage = "Maximum {1} characters")]
        public string Ledger_Name { get; set; }
        [Required]
        [Display(Name = "Account Group")]
        public int AccountGroup_Id { get; set; }
        [Display(Name = "Code")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} must be numeric")]
        public int Ledger_Code { get; set; }
        [Display(Name = "Currency")]
        public long Currency_Id { get; set; }
        [Display(Name = "Bill by bill")]
        public bool Ledger_BillByBill { get; set; }
        [Display(Name = "Status")]
        public int Ledger_Status { get; set; }
        [Display(Name = "Bank name")]
        [MaxLength(30, ErrorMessage = "Maximum {1} characters")]
        public string LedgerBank_BankName { get; set; }
        [Display(Name = "Branch name")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerBank_BranchName { get; set; }
        [Display(Name = "Branch code")]
        [MaxLength(20, ErrorMessage = "Maximum {1} characters")]
        public string LedgerBank_BranchCode { get; set; }
        [Display(Name = "Account number")]
        [MaxLength(30, ErrorMessage = "Maximum {1} characters")]
        public string LedgerBank_AccountNumber { get; set; }
        [Display(Name = "Account name")]
        [MaxLength(40, ErrorMessage = "Maximum {1} characters")]
        public string LedgerBank_AccountName { get; set; }
        [Display(Name = "IBAN")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerBank_IBAN { get; set; }
        [Display(Name = "Swift")]
        [MaxLength(30, ErrorMessage = "Maximum {1} characters")]
        public string LedgerBank_Swift { get; set; }
        [Display(Name = "Header note")]
        [MaxLength(70, ErrorMessage = "Maximum {1} characters")]
        public string LedgerBank_HeaderNote { get; set; }
        [Display(Name = "Footer note")]
        [MaxLength(70, ErrorMessage = "Maximum {1} characters")]
        public string LedgerBank_FooterNote { get; set; }
        [Display(Name = "Credit limit")]
        [DisplayFormat(DataFormatString = "{0:0.######}")]
        public decimal LedgerDetails_CreditLimit { get; set; }
        [Display(Name = "Credit period")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} must be numeric")]
        public int LedgerDetails_CreditPeriod { get; set; }
        [Display(Name = "Mailing name")]
        [MaxLength(150, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_MailingName { get; set; }
        [Display(Name = "Branch")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_Branch { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string LedgerDetails_Email { get; set; }
        [Display(Name = "Address")]
        [MaxLength(200, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_Address { get; set; }
        [Display(Name = "Contact person")]
        [MaxLength(70, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_ContactPerson { get; set; }
        [Display(Name = "Mobile")]
        [MaxLength(20, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_Mobile1 { get; set; }
        [Display(Name = "Second mobile")]
        [MaxLength(20, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_Mobile2 { get; set; }
        [Display(Name = "Phone")]
        [MaxLength(20, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_Phone { get; set; }
        [Display(Name = "Fax")]
        [MaxLength(20, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_Fax { get; set; }
        [Display(Name = "Narration")]
        [MaxLength(100, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_Narration { get; set; }
        [Display(Name = "IBAN")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_BankIBAN { get; set; }
        [Display(Name = "Branch name")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_BankBranchName { get; set; }
        [Display(Name = "Branch code")]
        [MaxLength(20, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_BankBranchCode { get; set; }
        [Display(Name = "Swift code")]
        [MaxLength(30, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_BankSwiftCode { get; set; }
        [Display(Name = "Account number")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_BankAccountNumber { get; set; }
        [Display(Name = "Cheque on name")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_BankNameOnCheque { get; set; }
        [Display(Name = "Ship to")]
        [MaxLength(200, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_ShipTo { get; set; }
        public int TermsAndCondition_Id { get; set; }
        [Display(Name = "CST")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_CST { get; set; }
        [Display(Name = "TIN")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_TIN { get; set; }
        [Display(Name = "VAT")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_VAT { get; set; }
        [Display(Name = "PAN")]
        [MaxLength(50, ErrorMessage = "Maximum {1} characters")]
        public string LedgerDetails_PAN { get; set; }
        public MessageViewModel OprMessage { get; set; }
        public AccountModel()
        {
            OprMessage = new MessageViewModel();

        }
    }
}
