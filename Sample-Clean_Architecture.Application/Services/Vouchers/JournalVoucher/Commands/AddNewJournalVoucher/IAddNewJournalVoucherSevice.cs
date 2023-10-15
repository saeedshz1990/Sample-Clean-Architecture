using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher
{
    public interface IAddNewJournalVoucherSevice
    {
        ResultDto<int> Execute(RequestJournalVoucher request);
    }
    public class AddNewJournalVoucherSevice : IAddNewJournalVoucherSevice
    {
        private readonly IDatabaseContext _context;


        public AddNewJournalVoucherSevice(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<int> Execute(RequestJournalVoucher request)
        {
            try
            {
                ResultJournalVoucher dto = _context.sp_Voucher_Insert(request);

                switch (dto.IsSuccess)
                {
                    case 1:
                        return new ResultDto<int>
                        {
                            IsSuccess = true,
                            Message = AppMessages.SUCCESS,
                            Data = dto.voucherMasters_Id
                        };

                    case 0:
                        return new ResultDto<int>
                        {
                            IsSuccess = false,
                            Message = AppMessages.ERROR,
                            Data = dto.IsSuccess
                        };
                    case 2:
                        return new ResultDto<int>
                        {
                            IsSuccess = false,
                            Message = AppMessages.VOUCHER_NOPRIVILEGE,
                            Data = dto.IsSuccess
                        };
                    case 3:
                        return new ResultDto<int>
                        {
                            IsSuccess = false,
                            Message = AppMessages.VOUCHER_NOPRIVILEGE_DATE,
                            Data = dto.IsSuccess
                        };
                    default:
                        return new ResultDto<int>
                        {
                            IsSuccess = false,
                            Message = AppMessages.ERROR

                        };

                }
            }
            catch (Exception ex)
            {

                return new ResultDto<int>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }

    public class JournalVoucherDto
    {
        public JournalVoucherMasterDto JournalVoucherMasterDto { set; get; }
        public List<JournalVoucherDetailDto> JournalVoucherDetailsDto { set; get; }
        public List<CurrencyCompany> CurrencyCompanyList { get; set; }
        public JournalVoucherDto()
        {
            JournalVoucherMasterDto = new JournalVoucherMasterDto();
            JournalVoucherDetailsDto = new List<JournalVoucherDetailDto>();
            CurrencyCompanyList = new List<CurrencyCompany>();
        }
    }

    public class JournalVoucherMasterDto
    {
        public long Id { set; get; }
        public string VoucherNo { set; get; }
        public string InvoiceNo { set; get; }
        public string RefNo { set; get; }
        public string RefNo2 { set; get; }
        public int Currency_Id { set; get; }
        public string VoucherDate { set; get; }
        public int Project_Id { set; get; }
        public int Branch_Id { set; get; }
        public int FinancialCycle_Id { set; get; }
        public string PublicNotes { set; get; }
        public string Notes { set; get; }
        public JournalVoucherMasterDto()
        {
            VoucherNo = "0";
            Id = 0;
        }
    }
    public class JournalVoucherDetailDto
    {
        public long Id { set; get; }
        public long Ledger_Id { set; get; }
        public decimal Debit { set; get; }
        public decimal Credit { set; get; }
        public long Currency_Id { set; get; }
        public long Rate_Id { set; get; }
        public decimal Rate { set; get; }
        public string ChequeNo { set; get; }
        public string ChequeDate { set; get; }
        public string Remark { set; get; }
        public int CostCenter_Id { set; get; }
        public int Type_Id { set; get; }
        public byte RecStatus { set; get; }
        public bool Ratechnage { set; get; }
    }
    public class RequestJournalVoucher
    {
        public string MasterInfo { get; set; }
        public byte voucherType_Id { get; set; }
        public int Users_Id { get; set; }
        public int Company_Id { get; set; }
        public string DetailInfo { get; set; }
        public decimal SecondRate { get; set; }
        public string CurrencyRateStr { get; set; }
    }
    public class ResultJournalVoucher
    {
        public int voucherMasters_Id { get; set; }
        public byte IsSuccess { get; set; }

    }

}