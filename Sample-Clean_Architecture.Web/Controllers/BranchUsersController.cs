using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserBranchAccess;
using Sample_Clean_Architecture.Application.Services.Users.Queries.GetUserBranchAccess;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models;
using Sample_Clean_Architecture.Web.Utilities;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class BranchUsersController : BaseController
    {
        private readonly IGetUserBranchAccessesService _getUserBranchAccessService;
        private readonly IUserAccessBranchService _userAccessBranchService;

        public BranchUsersController(IGetUserBranchAccessesService getUserBranchAccessService, IUserAccessBranchService userAccessBranchService)
        {
            _getUserBranchAccessService = getUserBranchAccessService;
            _userAccessBranchService = userAccessBranchService;
        }
        [HttpGet]
        public IActionResult Index(int id, byte kind)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ViewBag.Id = id;
            ViewBag.Kind = kind;
            /*
             * 1  Branch Users
             * 2  Cost Center Users
             * 3  Project Users
             */
            return View();
        }

        [HttpPost]
        public ActionResult Index(int id, byte kindopr, string selectedItems)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            List<JsTreeNode> items = JsonConvert.DeserializeObject<List<JsTreeNode>>(selectedItems);
            List<requesUserBranchDto> dtos = new List<requesUserBranchDto>();
            foreach (JsTreeNode node in items)
            {
                dtos.Add(new requesUserBranchDto()
                {
                    CompanyUsers_Id = node.id.ToInt()
                });
            }
            ResultDto resultDto = _userAccessBranchService.Execute(id, JsonConvert.SerializeObject(dtos), kindopr);
            if (resultDto.IsSuccess)
                return RedirectToAction("Index");
            else
                return Json(resultDto.Message);
        }


        [HttpPost("BranchUsers/GetTreeJson/{id}/{kind}")]
        public ActionResult GetTreeJson(int id, byte kind)
        {
            ActiveUser activeUser = CurrentUser.Get();
            var nodesList = new List<JsTreeNode>();
            ResultDto<List<ResultUserBranchDto>> result = _getUserBranchAccessService.Execute(id, activeUser.Company_Id, kind);

            foreach (ResultUserBranchDto dto in result.Data)
            {
                var rootNode = new JsTreeNode()
                {
                    id = dto.CompanyUsers_Id.ToString(),
                    text = dto.Users_UserName + " " + dto.Users_Description,
                    state = { selected = dto.Id > 0 }
                };
                nodesList.Add(rootNode);
            }
            return Json(nodesList);
        }
        public void PopulateTree(List<ResultUserBranchDto> dtos, JsTreeNode parentNode)
        {
            foreach (ResultUserBranchDto dto in dtos)
            {
                // if (dto.MenuOptions_ParentId == parentNode.id.ToInt())
                {
                    JsTreeNode node = new JsTreeNode()
                    {
                        id = dto.CompanyUsers_Id.ToString(),
                        text = dto.Users_UserName + " " + dto.Users_Description,
                        state = { selected = dto.Id > 0 },

                    };
                    parentNode.children.Add(node);
                    PopulateTree(dtos, node);

                }
            }


        }
    }
}
