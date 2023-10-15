using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.CostCenter.Commands.AddNewCostCenter
{
    public interface IAddNewCostCenterService
    {
        ResultDto Execute(int Company_Id, CostCenterDto costCenter);
    }
    public class AddNewCostCenterService : IAddNewCostCenterService
    {
        private readonly IDatabaseContext _context;

        public AddNewCostCenterService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(int Company_Id, CostCenterDto costCenter)
        {
            try
            {
                if (_context.sp_CostCenter_Insert(Company_Id, costCenter) == 2)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }
        }
    }
    public class CostCenterDto
    {
        public int CostCenter_Id { get; set; }
        public string CostCenter_Name { get; set; }
        public string CostCenter_Description { get; set; }
        public byte CostCenter_Status { get; set; }
        public List<CostCenterStatusDto> CostCenterStatusDto { get; set; }

        public CostCenterDto()
        {
            CostCenterStatusDto = new List<CostCenterStatusDto>();
        }
    }
    public class CostCenterStatusDto
    {
        public byte Status_Id { get; set; }
        public string Status_Description { get; set; }
    }
    public class UserCostCenterDto
    {
        public int CompanyUsers_Id { get; set; }
        public string Users_Username { get; set; }
        public int Users_Id { get; set; }
    }
}
