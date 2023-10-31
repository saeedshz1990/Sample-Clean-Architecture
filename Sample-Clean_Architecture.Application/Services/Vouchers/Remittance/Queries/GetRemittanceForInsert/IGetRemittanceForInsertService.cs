using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.GetRemittanceForInsert
{
    public interface IGetRemittanceForInsertService
    {
        ResultDto<List<RemittanceCurrenciesDto>> Execute(int Company_Id, long Currency_Id);

    }
    public class GetRemittanceForInsertService : IGetRemittanceForInsertService
    {
        private readonly IDatabaseContext _context;

        public GetRemittanceForInsertService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<List<RemittanceCurrenciesDto>> Execute(int Company_Id, long Currency_Id)
        {
            var data = _context.sp_Remittance_GetForInsert(Company_Id, Currency_Id);

            return new ResultDto<List<RemittanceCurrenciesDto>>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }



    public class RemittanceCurrenciesDto
    {
        public int RemmitenceBatch_Id { set; get; }
        public decimal RemmitenceBatch_Rate { set; get; }
        public decimal RemmitenceBatch_Remaining { set; get; }
        public decimal RemittanceSell_Amount { set; get; }
        public int RemittanceSell_Id { set; get; }
        public int RemittanceSell_AmountInserted { set; get; }
    }
    public class RemittanceCurrencies_Dto
    {
        public float Rate { set; get; }

        public List<RemittanceCurrenciesDto> RemittanceCurrenciesDto { set; get; }
    }
}
