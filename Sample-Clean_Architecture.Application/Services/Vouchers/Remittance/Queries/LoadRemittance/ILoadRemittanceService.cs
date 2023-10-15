using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.PaymentVoucher.Queries.LoadPaymentlVoucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.LoadRemittance
{
    public interface ILoadRemittanceService
    {
        ResultDto<RemittanceLoadDto> Execute(int Company_Id, int VoucherTypeId, bool CurrentDate, DateTime VoucherDate);

    }
    public class LoadRemittanceService : ILoadRemittanceService
    {
        private readonly IDatabaseContext _context;

        public LoadRemittanceService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<RemittanceLoadDto> Execute(int Company_Id, int VoucherTypeId, bool CurrentDate, DateTime VoucherDate)
        {
            var data = _context.sp_Remittance_LoadPage(Company_Id, VoucherTypeId, CurrentDate, VoucherDate);

            return new ResultDto<RemittanceLoadDto>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
    public class RemittanceLoadDto
    {
        public string VoucherNumber { get; set; }
        public DateTime VoucherDate { get; set; }
        public InFormAccess InFormAccess { get; set; }
        public List<CurrencyCompany> CurrencyCompanyList { get; set; }
        public List<AccountLedger> AccountLedgerList { get; set; }
        public List<PaymentReceiptAccount> CustomerAccountList { get; set; }
        public List<VoucherType> VoucherTypeList { get; set; }
        public RemittanceInfo RemittanceInfo { get; set; }
        public List<RemittanceAllCurrencies> RemittanceAllCurrencies { get; set; }
        public long DefaultCurrency_Id { get; set; }
        public RemittanceLoadDto()
        {
            InFormAccess = new InFormAccess();
            CurrencyCompanyList = new List<CurrencyCompany>();
            AccountLedgerList = new List<AccountLedger>();
            VoucherTypeList = new List<VoucherType>();
            CustomerAccountList = new List<PaymentReceiptAccount>();
            RemittanceInfo = new RemittanceInfo();
            RemittanceAllCurrencies = new List<RemittanceAllCurrencies>();
        }

    }

    public class RemittanceInfo
    {
        public byte Remittance_AmountDecimalNo { get; set; }
        public byte Remittance_RateDecimalNo { get; set; }
        public decimal Remittance_BalanceDifference { get; set; }
        public long Remittance_BalanceDifferenceLedger { get; set; }
        public string Ledger_Name { get; set; }
    }

    public class RemittanceAllCurrencies
    {
        public string CurrencyName { get; set; }
        public int RemmitenceBatch_Id { get; set; }
        public decimal RemmitenceBatch_Rate { get; set; }
        public decimal RemmitenceBatch_Remaining { get; set; }
        public decimal RemmitenceBatch_Tot { get; set; }
    }
}
