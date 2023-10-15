using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.CostCenter.DeleteCostCenter
{
    public interface IDeleteCostCenterService
    {
        ResultDto Execute(int costcenter_id);
    }

    public class DeleteCostCenterService : IDeleteCostCenterService
    {
        private readonly IDatabaseContext _context;


        public DeleteCostCenterService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(int costcenter_id)
        {
            try
            {
                bool error = false;
                if (_context.sp_CostCenter_Delete(costcenter_id, out error) == 2)
                {
                    if (error == true)
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
                            Message = AppMessages.UNABLE_DELETE,
                        };

                    }
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
}
