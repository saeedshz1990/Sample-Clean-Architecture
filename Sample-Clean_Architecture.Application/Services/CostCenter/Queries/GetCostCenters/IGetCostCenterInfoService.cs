using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.CostCenter.Commands.AddNewCostCenter;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.CostCenter.Queries.GetCostCenters
{
    public interface IGetCostCenterInfoService
    {
        ResultDto<CostCenterDto> Execute(int Company_Id, int CostCenter_Id);
    }

    public class GetCostCenterInfoService : IGetCostCenterInfoService
    {
        private readonly IDatabaseContext _context;

        public GetCostCenterInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<CostCenterDto> Execute(int Company_Id, int CostCenter_Id)
        {
            var cost_center = _context.sp_CostCenter_GetById(Company_Id, CostCenter_Id);

            return new ResultDto<CostCenterDto>()
            {
                Data = cost_center,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}
