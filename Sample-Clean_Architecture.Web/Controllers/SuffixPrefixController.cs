using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Commands.AddNewSuffixPrefix;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.LoadSuffixPrefix;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.SuffixPrefix;
using Sample_Clean_Architecture.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class SuffixPrefixController : BaseController
    {
        private readonly IAddNewSuffixPrefixService _addNewSuffixPrefixService;
        private readonly IGetSuffixPrefixInfoService _getSuffixPrefixInfoService;
        private readonly IGetSuffixPrefixService _getSuffixPrefixService;
        private readonly ILoadSuffixPrefixService _loadSuffixPrefixService;
        private readonly IGetListItemService _getListItemService;



        public SuffixPrefixController(IAddNewSuffixPrefixService addNewSuffixPrefixService, IGetSuffixPrefixInfoService getSuffixPrefixInfoService, IGetSuffixPrefixService getSuffixPrefixService, ILoadSuffixPrefixService loadSuffixPrefixService, IGetListItemService getListItemService)
        {
            _addNewSuffixPrefixService = addNewSuffixPrefixService;
            _getSuffixPrefixInfoService = getSuffixPrefixInfoService;
            _getSuffixPrefixService = getSuffixPrefixService;
            _loadSuffixPrefixService = loadSuffixPrefixService;
            _getListItemService = getListItemService;
        }
        public IActionResult Index()
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ResultDto<SuffixPrefixDto> result = _getSuffixPrefixService.Execute(activeUser.Company_Id);
            ViewBag.Company_Id = result.Data.Company_Id;
            return View(DtosToModels.SuffixPrefixToModel(result.Data.SuffixPrefixLst, activeUser.DateFormats_Description));

        }
        [NoDirectAccess]
        public IActionResult AddOrEdit(int id = 0, int cId = 0)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ResultDto<List<VoucherTypeDto>> resultDto = _loadSuffixPrefixService.Execute();
            if (resultDto.IsSuccess)
                ViewBag.VoucherTypes = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypes, resultDto.Data).Data);
            else
                ViewBag.VoucherTypes = new List<VoucherTypeDto>();

            ResultDto<SuffixPrefix_Dto> result = _getSuffixPrefixInfoService.Execute(id);
            if (result.IsSuccess)
            {

                result.Data.Company_Id = cId;
                return View(DtosToModels.SuffixPrefixToModel(result.Data, activeUser.DateFormats_Description));
            }
            else
                return Json(result);


        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, SuffixPrefixModel request)
        {

            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.VoucherTypes = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypes).Data);
            if (ModelState.IsValid)
            {
                ResultDto result = _addNewSuffixPrefixService.Execute(Utilities.ModelsToDtos.SuffixPrefixToDto(request, activeUser.DateFormats_Description.ToLower()));
                if (result.IsSuccess)
                {

                    //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.SuffixPrefixToModel(_getSuffixPrefixService.Execute(request.Company_Id).Data.SuffixPrefixLst)) });
                    request.OprMessage = new Models.MessageViewModel() { Message = result.Message, Color = AppMessages.GetMessageColor(MessageType.Success) };
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });

                }
                else
                {
                    //return Json(result);
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

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.SuffixPrefixToModel(_getSuffixPrefixService.Execute(activeUser.Company_Id).Data.SuffixPrefixLst, activeUser.DateFormats_Description)) });

        }
    }
}
