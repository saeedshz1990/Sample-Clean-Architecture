using Microsoft.AspNetCore.Mvc;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Application.Services.CostCenter.Commands.AddNewCostCenter;
using Sample_Clean_Architecture.Application.Services.CostCenter.DeleteCostCenter;
using Sample_Clean_Architecture.Application.Services.CostCenter.Queries.GetCostCenters;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.CostCenter;
using Sample_Clean_Architecture.Web.Utilities;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class CostCenterController : BaseController
    {
        private readonly IGetCostCenterService _getcostcenterservice;
        private readonly IAddNewCostCenterService _addNewCostCenterService;
        private readonly IGetCostCenterInfoService _getcostcenterinfoservice;
        private readonly IDeleteCostCenterService _deletecostcenterservice;
        private readonly IGetListItemService _getListItemService;
        public CostCenterController(IGetCostCenterService getcostcenterservice, IGetCostCenterInfoService getcostcenterinfoservice, IAddNewCostCenterService addNewCostCenterService, IDeleteCostCenterService deletecostcenterservice, IGetListItemService getListItemService)
        {
            _getcostcenterservice = getcostcenterservice;
            _addNewCostCenterService = addNewCostCenterService;
            _getcostcenterinfoservice = getcostcenterinfoservice;
            _deletecostcenterservice = deletecostcenterservice;
            _getListItemService = getListItemService;
        }
        public IActionResult Index()
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ResultDto<List<CostCenterListDto>> result = _getcostcenterservice.Execute(activeUser.Company_Id);
            return View(DtosToModels.CostCenterToModel(result.Data));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            ResultDto result = _deletecostcenterservice.Execute(id);
            if (result.IsSuccess)
            {
                ActiveUser activeUser = CurrentUser.Get();
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll",
                    DtosToModels.CostCenterToModel(_getcostcenterservice.Execute(activeUser.Company_Id).Data))
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

            ResultDto<CostCenterDto> result = _getcostcenterinfoservice.Execute(activeUser.Company_Id, id);
            if (result.IsSuccess)
            {
                ViewBag.CostCenterStatus = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterStatusList, result.Data.CostCenterStatusDto).Data);// new SelectList(result.Data.CostCenterStatusDto, "Status_Id", "Status_Description");
                return View(DtosToModels.CostCenterInfoToModel(result.Data));
            }
            else
                return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(CostCenterInfoModel request)
        {
            ViewBag.CostCenterStatus = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterStatusList).Data);

            if (ModelState.IsValid)
            {
                ActiveUser activeUser = CurrentUser.Get();

                ResultDto result = _addNewCostCenterService.Execute(activeUser.Company_Id, Utilities.ModelsToDtos.CostCenterInfoToDto(request));

                if (result.IsSuccess)
                {
                    //return Json(new
                    //{
                    //    isValid = true,
                    //    html = Helper.RenderRazorViewToString(this, "_ViewAll",
                    //     DtosToModels.CostCenterToModel(_getcostcenterservice.Execute(activeUser.Company_Id).Data))
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
                html = Helper.RenderRazorViewToString(this, "_ViewAll",
                 DtosToModels.CostCenterToModel(_getcostcenterservice.Execute(activeUser.Company_Id).Data))
            });
        }
    }
}
