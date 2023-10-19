using Sample_Clean_Architecture.Common.Dtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Voucher
{
    public class JournalVoucherModel
    {
        [Key]
        public long Id { set; get; }
        public string VoucherNo { set; get; }
        [DisplayName("Voucher No")]
        public string InvoiceNo { set; get; }

        [DisplayName("RefNo")]
        public string RefNo { set; get; }
        [DisplayName("RefNo2")]
        public string RefNo2 { set; get; }
        [DisplayName("Currency")]
        public int Currency_Id { set; get; }
        [DisplayName("Date")]
        //[BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        // [BindProperty, DataType(DataType.Date) ,Localizable(true)]
        public string VoucherDate { set; get; }
        [DisplayName("Project")]
        public int Project_Id { set; get; }
        [DisplayName("Branch")]
        public int Branch_Id { set; get; }
        [DisplayName("Public Notes")]
        public string PublicNotes { set; get; }
        [DisplayName("Notes")]
        public string Notes { set; get; }
        [DisplayName("Payment Account")]
        public long PaymentAccount_Id { set; get; }
        [DisplayName("Receipt Account")]
        public long ReceiptAccount_Id { set; get; }
        public bool ProjectActive { set; get; }
        public bool CostCenterActive { set; get; }
        public int VoucherType { set; get; }
        [DisplayName("TMN-AED")]
        public decimal TMN_AED { set; get; }

        public InFormAccess InFormAccess { set; get; }
        public List<JournalVoucherContentModel> JournalVoucherContentModel { set; get; }
        public JournalVoucherModel()
        {
            JournalVoucherContentModel = new List<JournalVoucherContentModel>();
            InFormAccess = new InFormAccess();
        }
    }
    public class JournalVoucherContentModel
    {
        [Key]
        public long Id { set; get; }
        [DisplayName("No")]
        public int No { set; get; }
        [DisplayName("Account Ledger")]
        public string AccountLedger_Id { set; get; }
        [DisplayName("Balance")]
        public int Balance { set; get; }

        [DisplayName("Dr/Cr")]
        public string DrCr_Id { set; get; }
        [DisplayName("Amount")]
        [Required(ErrorMessage = "{0} is required")]
        public decimal Amount { set; get; }
        [DisplayName("Currency")]
        public string Currency_Id { set; get; }
        [DisplayName("Exchange Rate")]
        public decimal ExchangeRate { set; get; }
        [DisplayName("Cheque No.")]
        public string ChequeNo { set; get; }
        [DisplayName("Cheque Date")]
        public string ChequeDate { set; get; }
        [DisplayName("Remark")]
        public string Remark { set; get; }
        [DisplayName("Cost Center")]
        public string CostCenter_Id { set; get; }
        [DisplayName("Type")]
        public string Type_Id { set; get; }
        public int RecStatus { set; get; }

        public string Rate_Id { set; get; }
        public decimal ExchangeRateOld { set; get; }
    }
}
