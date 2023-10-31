using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Users.Queries.GetUserAccesses
{
    public interface IGetUserAccessesService
    {
        ResultDto<List<ResultMenuDto>> Execute(int companyUsers_Id);
    }

    public class GetUserAccessesService : IGetUserAccessesService
    {
        private readonly IDatabaseContext _context;

        public GetUserAccessesService(IDatabaseContext context)
        {
            _context = context;
        }
        //دریافت لیست دسترسی های کاربرا ن از دیتابیس
        public ResultDto<List<ResultMenuDto>> Execute(int companyUsers_Id)
        {
            var userAccesses = _context.Sp_CompanyUsers_PolicyGet(companyUsers_Id).ToList();

            return new ResultDto<List<ResultMenuDto>>()
            {
                Data = userAccesses,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class ResultMenuDto
    {
        public int MenuOptions_Id { get; set; }
        public string MenuOptions_Title { get; set; }
        public int MenuOptions_ParentId { get; set; }
        public int CompanyUsers_MenuId { get; set; }
        //   public string MenuOptions_Icon { get; set; }
        //   public int MenuOptions_Order { get; set; }
        public bool MenuOptions_IsMenu { get; set; }
    }
    public class requestMenuDto
    {
        public int MenuOptions_Id { get; set; }

    }
}
