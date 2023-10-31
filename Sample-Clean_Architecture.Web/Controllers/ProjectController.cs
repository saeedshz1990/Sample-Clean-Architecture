using Microsoft.AspNetCore.Mvc;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Application.Services.Project.Commands.AddNewProject;
using Sample_Clean_Architecture.Application.Services.Project.Commands.DeleteProject;
using Sample_Clean_Architecture.Application.Services.Project.Queries.GetProjects;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Project;
using Sample_Clean_Architecture.Web.Utilities;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IGetProjectService _getProjectservice;
        private readonly IGetProjectInfoService _getprojectinfoservice;
        private readonly IAddNewProjectService _addNewProjectService;
        private readonly IDeleteProjectService _deleteProjectservice;
        private readonly IGetListItemService _getListItemService;
        public ProjectController(IGetProjectService getProjectservice, IGetProjectInfoService getprojectinfoservice,
            IAddNewProjectService addNewProjectService, IDeleteProjectService deleteProjectservice, IGetListItemService getListItemService)
        {
            _getProjectservice = getProjectservice;
            _getprojectinfoservice = getprojectinfoservice;
            _addNewProjectService = addNewProjectService;
            _deleteProjectservice = deleteProjectservice;
            _getListItemService = getListItemService;
        }
        public IActionResult Index()
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ResultDto<List<ProjectListDto>> result = _getProjectservice.Execute(activeUser.Company_Id);
            return View(DtosToModels.ProjectToModel(result.Data));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            ResultDto result = _deleteProjectservice.Execute(id);
            if (result.IsSuccess)
            {
                ActiveUser activeUser = CurrentUser.Get();
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.ProjectToModel(_getProjectservice.Execute(activeUser.Company_Id).Data))
                });
            }
            else
            {
                return Json(result);
            }
        }

        [HttpGet]
        [NoDirectAccess]
        public IActionResult AddOrEdit(int id)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);

            ActiveUser activeUser = CurrentUser.Get();

            ResultDto<ProjectDto> result = _getprojectinfoservice.Execute(activeUser.Company_Id, id);
            if (result.IsSuccess)
            {
                ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
                ViewBag.CostCenter = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList, result.Data.CostCenterList).Data);// new SelectList(result.Data.CostCenterList, "CostCenter_Id", "CostCenter_Name");
                ViewBag.ProjectStatus = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ProjectStatusList, result.Data.ProjectStatusList).Data);// new SelectList(result.Data.ProjectStatusList, "Status_Id", "Status_Description");
                return View(DtosToModels.ProjectInfoToModel(result.Data, activeUser.DateFormats_Description));
            }
            else
                return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(ProjectInfoModel request)
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.CostCenter = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList).Data);
            ViewBag.ProjectStatus = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ProjectStatusList).Data);

            if (ModelState.IsValid)
            {


                ResultDto result = _addNewProjectService.Execute(activeUser.Company_Id, Utilities.ModelsToDtos.ProjectInfoToDto(request, activeUser.DateFormats_Description.ToLower()));

                if (result.IsSuccess)
                {
                    //return Json(new
                    //{
                    //    isValid = true,
                    //    html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.ProjectToModel(_getProjectservice.Execute(activeUser.Company_Id).Data))
                    //});
                    request.OprMessage = new Models.MessageViewModel() { Message = result.Message, Color = AppMessages.GetMessageColor(MessageType.Success) };
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });

                }
                else
                {
                    // return Json(result);
                    request.OprMessage = new Models.MessageViewModel() { Message = result.Message, Color = AppMessages.GetMessageColor(MessageType.Warning) };
                    return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });

                }
            }
            request.OprMessage = new Models.MessageViewModel() { Message = AppMessages.REQUIRED, Color = AppMessages.GetMessageColor(MessageType.Error) };

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });
        }

        [HttpPost, ActionName("Close")]
        public IActionResult CloseDialog()
        {
            ActiveUser activeUser = CurrentUser.Get();

            return Json(new
            {
                isValid = true,
                html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.ProjectToModel(_getProjectservice.Execute(activeUser.Company_Id).Data))
            });
        }
    }
}
