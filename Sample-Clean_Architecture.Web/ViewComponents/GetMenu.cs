using Microsoft.AspNetCore.Mvc;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetMenuItem;
using Sample_Clean_Architecture.Web.Utilities;

namespace Sample_Clean_Architecture.Web.ViewComponents
{
    public class GetMenu : ViewComponent
    {

        public GetMenu()
        {

        }


        public IViewComponentResult Invoke()
        {


            ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");

            List<MenuItemDto> parentMenus = activeUser.Menus.Where(c => c.ParentId == 0).ToList();
            List<MenuItemDto> menus = new List<MenuItemDto>();
            foreach (MenuItemDto menuItem in parentMenus)
            {
                menus.Add(menuItem);
                List<MenuItemDto> Level2Menus = activeUser.Menus.Where(c => c.ParentId == menuItem.Id).ToList();
                menus.Find(c => c.Id == menuItem.Id).Childs.AddRange(Level2Menus);
                foreach (MenuItemDto l2MenuItem in Level2Menus)
                    menus.Find(c => c.Id == menuItem.Id).Childs.Find(c => c.Id == l2MenuItem.Id).Childs.AddRange(activeUser.Menus.Where(c => c.ParentId == l2MenuItem.Id));
            }

            menus.Insert(0, new MenuItemDto() { Id = 1000, ParentId = 0, Title = "Company Operations", Url = "#1000", Childs = new List<MenuItemDto>() { new MenuItemDto() { Id = 1001, ParentId = 1000, Title = "Company Operations", Url = "/Company/Index" } } });
            return View(viewName: "GetMenu", menus);
        }

    }
}
