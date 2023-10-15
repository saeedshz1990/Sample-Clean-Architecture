using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Application.Interfaces.Contexts;
using Demo.Application.Services.CostCenter.Commands.IAddNewCostCenterService;
using Demo.Common;
using Demo.Common.Dtos;

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
