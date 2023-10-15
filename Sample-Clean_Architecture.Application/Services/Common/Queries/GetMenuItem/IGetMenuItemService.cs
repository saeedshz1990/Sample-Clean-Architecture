using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Common.Queries.GetMenuItem
{
    public interface IGetMenuItemService
    {
        ResultDto<List<MenuItemDto>> Execute(int User_Id);
    }

    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDatabaseContext _context;
        public GetMenuItemService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<MenuItemDto>> Execute(int User_Id)
        {

            List<MenuItemDto> menus = _context.Menu_Get(User_Id).ToList().Select(p => new MenuItemDto
            {
                Id = p.MenuOptions_Id,
                Title = p.MenuOptions_Title,
                Url = p.MenuOptions_Url,
                ParentId = p.MenuOptions_ParentId
                /*,
                Childs = p..ToList().Select(child => new MenuItemDto
                {
                    Id = child.Id,
                    Name = child.Name,
                }).ToList(),*/
            }).ToList();

            return new ResultDto<List<MenuItemDto>>()
            {
                Data = menus,
                IsSuccess = true,
            };
        }
    }
    public interface IGetMenuOptionsService
    {
        ResultDto<List<MenuItemDto>> Execute();
    }

    public class GetMenuOptionsService : IGetMenuOptionsService
    {
        private readonly IDatabaseContext _context;

        public GetMenuOptionsService(IDatabaseContext context)
        {
            _context = context;
        }
        //دریافت لیست دسترسی های کاربرا ن از دیتابیس
        public ResultDto<List<MenuItemDto>> Execute()
        {
            var userAccesses = _context.MenuOption_GetAll().ToList();

            return new ResultDto<List<MenuItemDto>>()
            {
                Data = userAccesses,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }
        public List<MenuItemDto> Childs { get; set; }
        public MenuItemDto()
        {
            Childs = new List<MenuItemDto>();
        }
    }
}
