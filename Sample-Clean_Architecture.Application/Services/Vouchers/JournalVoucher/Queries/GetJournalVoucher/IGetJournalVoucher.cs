using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.GetJournalVoucher
{
    public interface IGetJournalVoucher
    {
        ResultDto<JournalVoucherDto> Execute(long voucherMasters_Id);

    }
    public class GetJournalVoucher : IGetJournalVoucher
    {
        private readonly IDatabaseContext _context;

        public GetJournalVoucher(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<JournalVoucherDto> Execute(long voucherMasters_Id)
        {
            var data = _context.sp_Voucher_GetById(voucherMasters_Id);

            return new ResultDto<JournalVoucherDto>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}
