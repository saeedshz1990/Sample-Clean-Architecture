using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.CostCenter.Queries.GetCostCenters
{
    public interface IGetCostCenterService
    {
        ResultDto<List<CostCenterListDto>> Execute(int Company_Id);
    }

    public class GetCostCenterService : IGetCostCenterService
    {
        private readonly IDatabaseContext _context;

        public GetCostCenterService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<List<CostCenterListDto>> Execute(int Company_Id)
        {
            List<CostCenterListDto> costcenters = _context.sp_CostCenter_List(Company_Id);

            return new ResultDto<List<CostCenterListDto>>()
            {
                Data = costcenters,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class CostCenterListDto
    {
        public int CostCenter_Id { get; set; }

        public string CostCenter_Name { get; set; }

        public string Status_Description { get; set; }
        public bool CostCenter_Used { get; set; }
    }
}
