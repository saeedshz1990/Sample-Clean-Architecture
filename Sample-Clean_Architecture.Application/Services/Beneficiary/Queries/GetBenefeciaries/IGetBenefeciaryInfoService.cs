using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.AddNewBenefeciary;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Beneficiary.Queries.GetBenefeciaries
{
    public interface IGetBenefeciaryInfoService
    {
        ResultDto<Benefeciary_Dto> Execute(int beneficiary_id);
    }
    public class GetBenefeciaryInfoService : IGetBenefeciaryInfoService
    {
        private readonly IDatabaseContext _context;


        public GetBenefeciaryInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<Benefeciary_Dto> Execute(int beneficiary_id)
        {
            try
            {
                Benefeciary_Dto benefeciary = _context.sp_Beneficiary_GetById(beneficiary_id);
                if (benefeciary != null)
                {
                    return new ResultDto<Benefeciary_Dto>
                    {
                        Data = benefeciary,
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto<Benefeciary_Dto>
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<Benefeciary_Dto>
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }
}
