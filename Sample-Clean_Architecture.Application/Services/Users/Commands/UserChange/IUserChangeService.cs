using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetMenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Users.Commands.UserChange
{
    public interface IUserChangeService
    {
        ResultDto<ResultUserChangeDto> Execute(int companyUsers_Id);
    }
    public class UserChangeService : IUserChangeService
    {
        private readonly IDatabaseContext _context;
        public UserChangeService(IDatabaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultUserChangeDto> Execute(int companyUsers_Id)
        {
            ResultUserChangeDto resultUserChangeDto = _context.Sp_CompanyUsers_Change(companyUsers_Id);
            if (resultUserChangeDto != null)
            {
                return new ResultDto<ResultUserChangeDto>()
                {
                    Data = resultUserChangeDto,
                    IsSuccess = true,
                    Message = AppMessages.SUCCESS,
                };
            }
            else
            {
                return new ResultDto<ResultUserChangeDto>()
                {

                    IsSuccess = false,
                    Message = AppMessages.ERROR
                };
            }
        }
    }
    public class ResultUserChangeDto
    {
        public int CompanyUsers_Id { get; set; }
        public int Company_Id { get; set; }
        public List<MenuItemDto> Menus { get; set; }
        public byte Company_TransactionType { get; set; }
        public byte DateFormats_Id { get; set; }
        public byte Company_DateSeperator { get; set; }
        public DateTime FinancialCycle_FromDate { get; set; }
        public DateTime FinancialCycle_ToDate { get; set; }

        public ResultUserChangeDto()
        {
            Menus = new List<MenuItemDto>();
        }
    }
}

