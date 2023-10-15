using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
