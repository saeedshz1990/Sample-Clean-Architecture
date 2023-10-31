using Microsoft.AspNetCore.Mvc;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Commands.AddNewExchangeRate;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Commands.DeleteExchangeRate;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Queries.GetExchangeRate;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.ExchangeRate;
using Sample_Clean_Architecture.Web.Utilities;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class ExchangeRateController : BaseController
    {

        private readonly IGetExchangeRateService _getExchangeRateService;
        private readonly IGetExchangeRateInfoService _getExchangeRateinfoService;
        private readonly IAddNewExchangeRateService _addnewExchangeEateService;
        private readonly IDeleteExchangeRateService _deleteExchangeRateService;
        private readonly IGetListItemService _getListItemService;
        public ExchangeRateController(
            IGetExchangeRateService getExchangeRateService, 
            IGetExchangeRateInfoService getExchangeRateinfoService, 
            IAddNewExchangeRateService addnewExchangeEateService, 
            IDeleteExchangeRateService deleteExchangeRateService, 
            IGetListItemService getListItemService)
        {
            _getExchangeRateService = getExchangeRateService;
            _getExchangeRateinfoService = getExchangeRateinfoService;
            _addnewExchangeEateService = addnewExchangeEateService;
            _deleteExchangeRateService = deleteExchangeRateService;
            _getListItemService = getListItemService;
        }

        public IActionResult Index(DateTime? dateTime)
        {
            return fillGrid(dateTime);
        }

        private IActionResult fillGrid(DateTime? dateTime)
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ResultDto<ExchangeRate_Dto> result = _getExchangeRateService.Execute(activeUser.Company_Id, dateTime == null ? new DateTime(2079, 1, 1) : dateTime);
            return View(DtosToModels.ExchangeRateToModel(result.Data, activeUser.DateFormats_Description));
        }

        [HttpPost]
        public IActionResult Index(ExchangeRateModel model)
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewData["IsRendred"] = CheckIsRendred(Request);
            DateTime dateTime = model.VoucherDate.ToSystemFormat(activeUser.DateFormats_Description.ToLower()).ToDateTime();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ResultDto<ExchangeRate_Dto> result = _getExchangeRateService.Execute(activeUser.Company_Id, dateTime);
            return View(DtosToModels.ExchangeRateToModel(result.Data, activeUser.DateFormats_Description));
        }

        [HttpGet]
        [NoDirectAccess]
        public IActionResult AddOrEdit(long id, string dateTime)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);

            ActiveUser activeUser = CurrentUser.Get();
            DateTime dt = dateTime.ToSystemFormat(activeUser.DateFormats_Description.ToLower()).ToDateTime();

            ResultDto<ExchangeRateInfo_Dto> result = _getExchangeRateinfoService.Execute(activeUser.Company_Id, id, dt);
            if (result.IsSuccess)
            {
                ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
                ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ExchangeRateInfoDetail, result.Data.ExchangeRateInfoDetail_Dto).Data); // new SelectList(result.Data.ExchangeRateInfoDetail_Dto, "Currency_Id", "Currency_Name");

                return View(DtosToModels.ExchangeRateInfoToModel(result.Data.ExchangeRateInfoById, activeUser.DateFormats_Description));
            }
            else
                return Json(result);
        }
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(ExchangeRateInfoModel request)
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ExchangeRateInfoDetail).Data); // new SelectList(result.Data.ExchangeRateInfoDetail_Dto, "Currency_Id", "Currency_Name");

            if (ModelState.IsValid)
            {


                ResultDto result = _addnewExchangeEateService.Execute(Utilities.ModelsToDtos.ExchangeRateInfoToDto(request, activeUser.DateFormats_Description.ToLower()));
                DateTime dt = request.ExchangeRate_Date.ToSystemFormat(activeUser.DateFormats_Description.ToLower()).ToDateTime();

                if (result.IsSuccess)
                {
                    //return Json(new
                    //{
                    //    isValid = true,
                    //    html = Helper.RenderRazorViewToString(this, "_ViewAll",
                    //    DtosToModels.ExchangeRateToModel(_getExchangeRateService.Execute(activeUser.Company_Id, dt).Data, activeUser.DateFormats_Description))
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
        public IActionResult CloseDialog(string dateTime)
        {
            ActiveUser activeUser = CurrentUser.Get();

            //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CurrencyToModel(_getCurrencyService.Execute(activeUser.Company_Id).Data)) });
            DateTime dt = dateTime.ToSystemFormat(activeUser.DateFormats_Description.ToLower()).ToDateTime();

            return Json(new
            {
                isValid = true,
                html = Helper.RenderRazorViewToString(this, "_ViewAll",
                        DtosToModels.ExchangeRateToModel(_getExchangeRateService.Execute(activeUser.Company_Id, dt).Data, activeUser.DateFormats_Description))
            });
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id, string dateTime)
        {
            ResultDto result = _deleteExchangeRateService.Execute(id);
            if (result.IsSuccess)
            {
                ActiveUser activeUser = CurrentUser.Get();
                DateTime dt = dateTime.ToSystemFormat(activeUser.DateFormats_Description.ToLower()).ToDateTime();
                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll",
                         DtosToModels.ExchangeRateToModel(_getExchangeRateService.Execute(activeUser.Company_Id, dt).Data, activeUser.DateFormats_Description))
                });
            }
            else
            {
                return Json(result);
            }
        }
    }
}

