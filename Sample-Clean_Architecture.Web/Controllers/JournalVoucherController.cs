using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.GetJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.NavigateJornalVoucher;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Voucher;
using Sample_Clean_Architecture.Web.Utilities;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class JournalVoucherController : BaseController
    {
        private readonly IGetListItemService _getListItemService;
        private readonly ILoadJournalVoucherService _loadJournalVoucherService;
        private readonly IAddNewJournalVoucherSevice _addNewJournalVoucherSevice;
        private readonly IGetJournalVoucher _getJournalVoucher;
        private readonly INavigateJornalVoucherService _navigateJornalVoucherService;

        public JournalVoucherController(IGetListItemService getListItemService, ILoadJournalVoucherService loadJournalVoucherService, IAddNewJournalVoucherSevice addNewJournalVoucherSevice, IGetJournalVoucher getJournalVoucher, INavigateJornalVoucherService navigateJornalVoucherService)
        {
            _getListItemService = getListItemService;
            _loadJournalVoucherService = loadJournalVoucherService;
            _addNewJournalVoucherSevice = addNewJournalVoucherSevice;
            _getJournalVoucher = getJournalVoucher;
            _navigateJornalVoucherService = navigateJornalVoucherService;
        }

        public IActionResult Index()
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
            ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();
            JournalVoucherModel journalVoucherModel = new JournalVoucherModel();
            ResultDto<JournalVoucherLoadDto> result = _loadJournalVoucherService.Execute(activeUser.Company_Id, activeUser.Users_Id, activeUser.CompanyUsers_Id, false, DateTime.Now);
            if (result.IsSuccess)
            {
                journalVoucherModel.InvoiceNo = result.Data.VoucherNumber;
                journalVoucherModel.VoucherDate = result.Data.VoucherDate.ToCustomFormat(activeUser.DateFormats_Description);
                journalVoucherModel.CostCenterActive = result.Data.CostCenterActive;
                journalVoucherModel.ProjectActive = result.Data.ProjectActive;
                journalVoucherModel.JournalVoucherContentModel = new List<JournalVoucherContentModel>();
                journalVoucherModel.JournalVoucherContentModel.Add(new JournalVoucherContentModel() { Id = 0, AccountLedger_Id = "0", CostCenter_Id = "0", Currency_Id = "0", DrCr_Id = "0", Type_Id = "0", Amount = 0, Balance = 0, ChequeDate = Convert.ToDateTime("2079-01-01").ToCustomFormat(activeUser.DateFormats_Description), ChequeNo = "0", ExchangeRate = 0, ExchangeRateOld = 0, No = 1, Remark = "", RecStatus = 0 });
                ViewBag.AccountLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountLedgerList, result.Data.AccountLedgerList).Data);
                ViewBag.Projects = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ProjectList, result.Data.ProjectList).Data);
                ViewBag.Branches = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CompanyBranchList, result.Data.CompanyBranchList).Data);
                ViewBag.Types = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypeList, result.Data.VoucherTypeList).Data);
                ViewBag.DrCr = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.DrCr).Data);
                ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList, result.Data.CurrencyCompanyList).Data);
                ViewBag.CostCenters = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList, result.Data.CostCenterList).Data);
            }
            return View(journalVoucherModel);
        }
        [HttpPost]
        public IActionResult Index(JournalVoucherModel model)
        {
            ActiveUser activeUser = CurrentUser.Get();
            JournalVoucherDto journalVoucherDto = Utilities.ModelsToDtos.JournalVoucherToDto(model, activeUser.DateFormats_Description.ToLower());

            ResultDto<int> result = _addNewJournalVoucherSevice.Execute(new RequestJournalVoucher()
            {
                MasterInfo = JsonConvert.SerializeObject(journalVoucherDto.JournalVoucherMasterDto),
                DetailInfo = JsonConvert.SerializeObject(journalVoucherDto.JournalVoucherDetailsDto),
                Company_Id = activeUser.Company_Id,
                Users_Id = activeUser.Users_Id,
                voucherType_Id = 6
            });
            if (result.IsSuccess)
            {

                ViewBag.AccountLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountLedgerList).Data);
                ViewBag.Projects = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ProjectList).Data);
                ViewBag.Branches = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CompanyBranchList).Data);
                ViewBag.Types = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypeList).Data);
                ViewBag.DrCr = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.DrCr).Data);
                ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);
                ViewBag.CostCenters = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList).Data);

                ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
                ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
                ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();
                ViewBag.voucherMasters_Id = result.Data.ToInt();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", model) });
                // return View(model);

            }
            else
            {
                return Json(result.Message);
            }
        }

        [NoDirectAccess]
        public IActionResult VoucherList()
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            // Get Voucher List From Database

            //ResultDto<CompanyFinancialCycle_Dto> result = _companyFacad.GetCompanyFinancialCycleInfoService.Execute(id);
            //if (result.IsSuccess)
            // {
            //  return View(DtosToModels.CompanyFinancialCycleToModel(result.Data));
            return View(new VoucherDialogModel() { Id = 0, VoucherListModel = new List<VoucherListModel>() { new VoucherListModel() { Id = 5, VoucherDate = "2020-01-01", VoucherNo = "Ys123" }, new VoucherListModel() { Id = 6, VoucherDate = "2020-01-01", VoucherNo = "Ys2222" } } });
            // }
            // else
            //   return Json(result);


        }
        [HttpPost]
        public IActionResult LoadVoucher(long Id, string navType)
        {
            byte navigate_Status = 10;
            if (navType != null)
            {
                switch (navType)
                {
                    case "first":
                        navigate_Status = 1;
                        break;

                    case "pre":
                        navigate_Status = 2;
                        break;

                    case "next":
                        navigate_Status = 3;
                        break;

                    case "last":
                        navigate_Status = 4;
                        break;

                }
            }
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
            ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();
            JournalVoucherModel journalVoucherModel = new JournalVoucherModel();
            ResultDto<JournalVoucherDto> resultDto;
            if (navigate_Status == 10)
                resultDto = _getJournalVoucher.Execute(Id);
            else
                resultDto = _navigateJornalVoucherService.Execute(6, Id, navigate_Status);

            if (resultDto.IsSuccess)
            {
                journalVoucherModel = Utilities.DtosToModels.JournalVoucherToModel(resultDto.Data, activeUser.DateFormats_Description);
                journalVoucherModel.JournalVoucherContentModel.Add(new JournalVoucherContentModel() { Id = 0, AccountLedger_Id = "0", CostCenter_Id = "0", Currency_Id = "0", DrCr_Id = "0", Type_Id = "0", Amount = 0, Balance = 0, ChequeDate = Convert.ToDateTime("2079-01-01").ToCustomFormat(activeUser.DateFormats_Description), ChequeNo = "0", ExchangeRate = 0, ExchangeRateOld = 0, No = 0, Remark = "", RecStatus = 0 });
            }
            else
            {
                return Json(resultDto);
            }
            ViewBag.AccountLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountLedgerList).Data);
            ViewBag.Projects = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ProjectList).Data);
            ViewBag.Branches = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CompanyBranchList).Data);
            ViewBag.Types = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypeList).Data);
            ViewBag.DrCr = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.DrCr).Data);
            ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);
            ViewBag.CostCenters = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList).Data);

            ViewBag.JurnalVoucher_Id = journalVoucherModel.Id;

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", journalVoucherModel) });

        }
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult VoucherList(long Id)
        {
            return LoadVoucher(Id, null);
        }
    }
}
