using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Sample_Clean_Architecture.Application.Services.Users.Queries.GetUserBranchAccess
{
    public interface IGetUserBranchAccessesService
    {
        ResultDto<List<ResultUserBranchDto>> Execute(int Id, int company_Id, byte kind);
    }
    public class GetUserBranchAccessesService : IGetUserBranchAccessesService
    {
        private readonly IDatabaseContext _context;

        public GetUserBranchAccessesService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultUserBranchDto>> Execute(int Id, int company_Id, byte kind)
        {
            var userAccesses = _context.sp_UsersAccess_Get(Id, company_Id, kind).ToList();

            return new ResultDto<List<ResultUserBranchDto>>()
            {
                Data = userAccesses,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class ResultUserBranchDto
    {
        public int CompanyUsers_Id { get; set; }
        public string Users_UserName { get; set; }
        public string Users_Description { get; set; }
        public int Id { get; set; }
    }
    public class requesUserBranchDto
    {
        public int CompanyUsers_Id { get; set; }

    }
}
