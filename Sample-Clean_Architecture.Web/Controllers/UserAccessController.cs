using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserAccess;
using Sample_Clean_Architecture.Application.Services.Users.Queries.GetUserAccesses;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class UserAccessController : Controller
    {

        private readonly IGetUserAccessesService _getUserAccessService;
        private readonly IUserAccessService _userAccessService;

        public UserAccessController(
            IGetUserAccessesService getUserAccessService, 
            IUserAccessService userAccessService)
        {
            _getUserAccessService = getUserAccessService;
            _userAccessService = userAccessService;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            ViewBag.CompanyUsers_Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int id, string selectedItems)
        {
            if (selectedItems == null) return View();
            List<JsTreeNode> items = JsonConvert.DeserializeObject<List<JsTreeNode>>(selectedItems);
            List<requestMenuDto> dtos = new List<requestMenuDto>();
            foreach (JsTreeNode node in items)
            {
                dtos.Add(new requestMenuDto()
                {
                    MenuOptions_Id = node.id.ToInt()
                });
            }
            ResultDto resultDto = _userAccessService.Execute(id, JsonConvert.SerializeObject(dtos));
            if (resultDto.IsSuccess)
            {
                //return RedirectToAction("Index");
                ViewBag.CompanyUsers_Id = id;
                return View();
                // return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index") });

            }
            else
                return Json(resultDto.Message);
        }
        [HttpPost]
        public ActionResult GetTreeJson(int id)
        {
            var nodesList = new List<JsTreeNode>();
            ResultDto<List<ResultMenuDto>> result = _getUserAccessService.Execute(id);

            foreach (ResultMenuDto dto in result.Data)
            {
                if (dto.MenuOptions_ParentId == 0)
                {
                    var rootNode = new JsTreeNode()
                    {
                        id = dto.MenuOptions_Id.ToString(),
                        text = dto.MenuOptions_Title,
                        // state = {selected= dto.CompanyUsers_MenuId>0}
                    };
                    nodesList.Add(rootNode);
                    PopulateTree(result.Data, rootNode);
                }

            }


            return Json(nodesList);
        }
        public void PopulateTree(List<ResultMenuDto> dtos, JsTreeNode parentNode)
        {
            foreach (ResultMenuDto dto in dtos)
            {
                if (dto.MenuOptions_ParentId == parentNode.id.ToInt())
                {
                    JsTreeNode node = new JsTreeNode()
                    {
                        id = dto.MenuOptions_Id.ToString(),
                        text = dto.MenuOptions_Title,
                        state = { selected = dto.CompanyUsers_MenuId > 0 },
                        icon = dto.MenuOptions_Title == "Edit" ? Url.Content("~/media/image/bookmark_book_open.png") : dto.MenuOptions_Title == "Add" ? Url.Content("~/media/image/add.png") : dto.MenuOptions_Title == "Delete" ? Url.Content("~/media/image/delete.png") : "",
                    };

                    if (node.state.selected == true)
                        parentNode.state.selected = false;

                    parentNode.children.Add(node);
                    PopulateTree(dtos, node);

                }
            }


        }

    }
}
