using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Vouchers.PaymentVoucher.Queries.LoadPaymentlVoucher
{
    public interface ILoadPaymentVoucherService
    {
        ResultDto<OtherVoucherLoadDto> Execute(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);

    }
    public class LoadPaymentVoucherService : ILoadPaymentVoucherService
    {
        private readonly IDatabaseContext _context;

        public LoadPaymentVoucherService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<OtherVoucherLoadDto> Execute(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate)
        {
            var data = _context.sp_Voucher_PaymentLoad(Company_Id, Users_Id, CompanyUsers_Id, CurrentDate, VoucherDate);

            return new ResultDto<OtherVoucherLoadDto>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
    public class OtherVoucherLoadDto
    {
        public string VoucherNumber { get; set; }
        public DateTime VoucherDate { get; set; }
        public bool ProjectActive { get; set; }
        public bool CostCenterActive { get; set; }
        public InFormAccess InFormAccess { get; set; }
        public List<CurrencyCompany> CurrencyCompanyList { get; set; }
        public List<CompanyBranch> CompanyBranchList { get; set; }
        public List<JournalVoucher.Queries.LoadJournalVoucher.Project> ProjectList { get; set; }
        public List<AccountLedger> AccountLedgerList { get; set; }
        public List<PaymentReceiptAccount> PaymentReceiptAccountList { get; set; }
        public List<JournalVoucher.Queries.LoadJournalVoucher.CostCenter> CostCenterList { get; set; }
        public List<VoucherType> VoucherTypeList { get; set; }

        public OtherVoucherLoadDto()
        {
            InFormAccess = new InFormAccess();
            CurrencyCompanyList = new List<CurrencyCompany>();
            CompanyBranchList = new List<CompanyBranch>();
            ProjectList = new List<JournalVoucher.Queries.LoadJournalVoucher.Project>();
            AccountLedgerList = new List<AccountLedger>();
            CostCenterList = new List<JournalVoucher.Queries.LoadJournalVoucher.CostCenter>();
            VoucherTypeList = new List<VoucherType>();
            PaymentReceiptAccountList = new List<PaymentReceiptAccount>();
        }

    }
    //public class PaymentVoucherLoadDto
    //{
    //    public string VoucherNumber { get; set; }
    //    public DateTime VoucherDate { get; set; }
    //    public bool ProjectActive { get; set; }
    //    public bool CostCenterActive { get; set; }
    //    public InFormAccess InFormAccess { get; set; }
    //    public List<CurrencyCompany> CurrencyCompanyList { get; set; }
    //    public List<CompanyBranch> CompanyBranchList { get; set; }
    //    public List<Demo.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher.Project> ProjectList { get; set; }
    //    public List<AccountLedger> AccountLedgerList { get; set; }
    //    public List<PaymentReceiptAccount> PaymentReceiptAccountList { get; set; }
    //    public List<Demo.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher.CostCenter> CostCenterList { get; set; }
    //    public List<VoucherType> VoucherTypeList { get; set; }

    //    public PaymentVoucherLoadDto()
    //    {
    //        InFormAccess = new InFormAccess();
    //        CurrencyCompanyList = new List<CurrencyCompany>();
    //        CompanyBranchList = new List<CompanyBranch>();
    //        ProjectList = new List<Demo.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher.Project>();
    //        AccountLedgerList = new List<AccountLedger>();
    //        CostCenterList = new List<Demo.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher.CostCenter>();
    //        VoucherTypeList = new List<VoucherType>();
    //    }

    //}
    public class PaymentReceiptAccount
    {
        public int Ledger_Id { get; set; }
        public string Ledger_Name { get; set; }

    }
}
