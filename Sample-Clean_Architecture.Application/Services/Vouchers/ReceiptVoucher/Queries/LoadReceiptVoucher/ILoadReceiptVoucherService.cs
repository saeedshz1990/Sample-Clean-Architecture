using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Vouchers.PaymentVoucher.Queries.LoadPaymentlVoucher;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Vouchers.ReceiptVoucher.Queries.LoadReceiptVoucher
{
    public interface ILoadReceiptVoucherService
    {
        ResultDto<OtherVoucherLoadDto> Execute(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);

    }
    public class LoadReceiptVoucherService : ILoadReceiptVoucherService
    {
        private readonly IDatabaseContext _context;

        public LoadReceiptVoucherService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<OtherVoucherLoadDto> Execute(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate)
        {
            var data = _context.sp_Voucher_ReceiptLoad(Company_Id, Users_Id, CompanyUsers_Id, CurrentDate, VoucherDate);

            return new ResultDto<OtherVoucherLoadDto>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }


    //public class ReceiptAccount
    //{
    //    public int Ledger_Id { get; set; }
    //    public string Ledger_Name { get; set; }

    //}
}
