using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.GetJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.NavigateJornalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.PaymentVoucher.Queries.LoadPaymentlVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.ReceiptVoucher.Queries.LoadReceiptVoucher;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Voucher;
using Sample_Clean_Architecture.Web.Utilities;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class OtherVoucherController : BaseController
    {
        private readonly IGetListItemService _getListItemService;
        private readonly ILoadPaymentVoucherService _loadPaymentVoucherService;
        private readonly ILoadReceiptVoucherService _loadReceiptVoucherService;

        private readonly IAddNewJournalVoucherSevice _addNewJournalVoucherSevice;
        private readonly IGetJournalVoucher _getJournalVoucher;
        private readonly INavigateJornalVoucherService _navigateJornalVoucherService;

        public OtherVoucherController(IGetListItemService getListItemService, ILoadPaymentVoucherService loadPaymentVoucherService, ILoadReceiptVoucherService loadReceiptVoucherService, IAddNewJournalVoucherSevice addNewJournalVoucherSevice, IGetJournalVoucher getJournalVoucher, INavigateJornalVoucherService navigateJornalVoucherService)
        {
            _getListItemService = getListItemService;
            _loadPaymentVoucherService = loadPaymentVoucherService;
            _loadReceiptVoucherService = loadReceiptVoucherService;
            _navigateJornalVoucherService = navigateJornalVoucherService;
            _addNewJournalVoucherSevice = addNewJournalVoucherSevice;
            _getJournalVoucher = getJournalVoucher;
        }

        [HttpGet("OtherVoucher/Index/{voucherType}")]
        public IActionResult Index(int voucherType)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = CurrentUser.Get();
            ResultDto<OtherVoucherLoadDto> result = new ResultDto<OtherVoucherLoadDto>();
            if (voucherType == 1)
                result = _loadReceiptVoucherService.Execute(activeUser.Company_Id, activeUser.Users_Id, activeUser.CompanyUsers_Id, activeUser.IsCurrentDate, activeUser.WorkDay);
            else if (voucherType == 2)
                result = _loadPaymentVoucherService.Execute(activeUser.Company_Id, activeUser.Users_Id, activeUser.CompanyUsers_Id, activeUser.IsCurrentDate, activeUser.WorkDay);

            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
            ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();

            JournalVoucherModel journalVoucherModel = new JournalVoucherModel();


            if (result.IsSuccess)
            {
                //ResultDto<JournalVoucherDto> resultDto = _getJournalVoucher.Execute(10012);
                //if (resultDto.IsSuccess)
                //{
                //    if (resultDto.Data.JournalVoucherMasterDto.Id == 0)
                //    {
                journalVoucherModel.InvoiceNo = result.Data.VoucherNumber;
                journalVoucherModel.VoucherDate = result.Data.VoucherDate.ToCustomFormat(activeUser.DateFormats_Description);
                journalVoucherModel.CostCenterActive = result.Data.CostCenterActive;
                journalVoucherModel.ProjectActive = result.Data.ProjectActive;
                journalVoucherModel.JournalVoucherContentModel = new List<JournalVoucherContentModel>();
                journalVoucherModel.JournalVoucherContentModel.Add(new JournalVoucherContentModel() { Id = 0, AccountLedger_Id = "0", CostCenter_Id = "0", Currency_Id = "0", DrCr_Id = "0", Type_Id = "0", Amount = 0, Balance = 0, ChequeDate = Convert.ToDateTime("2079-01-01").ToCustomFormat(activeUser.DateFormats_Description), ChequeNo = "0", ExchangeRate = 0, ExchangeRateOld = 0, No = 0, Remark = "", RecStatus = 0 });
                if (voucherType == 1)
                    journalVoucherModel.ReceiptAccount_Id = -1;
                else
                    journalVoucherModel.PaymentAccount_Id = -1;

                journalVoucherModel.VoucherType = voucherType;
                // }
                //  else
                //  {
                //     journalVoucherModel = Utilities.DtosToModels.JournalVoucherToModel(resultDto.Data, activeUser.DateFormats_Description, voucherType);
                //     journalVoucherModel.JournalVoucherContentModel.Add(new JournalVoucherContentModel() { Id = 0, AccountLedger_Id = "0", CostCenter_Id = "0", Currency_Id = "0", DrCr_Id = "0", Type_Id = "0", Amount = 0, Balance = 0, ChequeDate = Convert.ToDateTime("12-10-2020").ToCustomFormat(activeUser.DateFormats_Description), ChequeNo = "0", ExchangeRate = 1, ExchangeRateOld = 1, No = 1, Remark = "", RecStatus = 0 });

                // }
            }

            ViewBag.PaymentReceiptAccount = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.PaymentReceiptAccountList, result.Data.PaymentReceiptAccountList).Data);
            ViewBag.AccountLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountLedgerList, result.Data.AccountLedgerList).Data);
            ViewBag.Projects = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ProjectList, result.Data.ProjectList).Data);
            ViewBag.Branches = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CompanyBranchList, result.Data.CompanyBranchList).Data);
            ViewBag.Types = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypeList, result.Data.VoucherTypeList).Data);
            // ViewBag.DrCr = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.DrCr).Data);
            ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList, result.Data.CurrencyCompanyList).Data);
            ViewBag.CostCenters = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList, result.Data.CostCenterList).Data);

            //     }


            return View(journalVoucherModel);
        }
        [HttpPost]
        public IActionResult Index(JournalVoucherModel model)
        {
            byte voucherType_Id = 4;
            string lstDrCr_Id = "1";
            if (model.ReceiptAccount_Id != 0)
            {
                lstDrCr_Id = "2";
                voucherType_Id = 5;
            }

            model.JournalVoucherContentModel.RemoveAll(c => c.AccountLedger_Id == "0");
            model.JournalVoucherContentModel.ForEach(c => c.DrCr_Id = lstDrCr_Id);

            model.JournalVoucherContentModel.Insert(0, new JournalVoucherContentModel() { DrCr_Id = voucherType_Id == 5 ? "1" : "2", Amount = model.JournalVoucherContentModel.Sum(c => c.Amount) });
            ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
            JournalVoucherDto journalVoucherDto = Utilities.ModelsToDtos.JournalVoucherToDto(model, activeUser.DateFormats_Description.ToLower());


            ResultDto<int> result = _addNewJournalVoucherSevice.Execute(new RequestJournalVoucher()
            {
                MasterInfo = JsonConvert.SerializeObject(journalVoucherDto.JournalVoucherMasterDto),
                DetailInfo = JsonConvert.SerializeObject(journalVoucherDto.JournalVoucherDetailsDto),
                Company_Id = activeUser.Company_Id,
                Users_Id = activeUser.Users_Id,
                voucherType_Id = voucherType_Id
            });
            if (result.IsSuccess)
            {

                ViewBag.PaymentReceiptAccount = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.PaymentReceiptAccountList).Data);
                ViewBag.AccountLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountLedgerList).Data);
                ViewBag.Projects = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ProjectList).Data);
                ViewBag.Branches = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CompanyBranchList).Data);
                ViewBag.Types = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypeList).Data);
                ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);
                ViewBag.CostCenters = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList).Data);
                ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
                ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
                ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();
                ViewBag.voucherMasters_Id = result.Data.ToInt();
                // return View(model);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", model) });

            }
            else
            {
                return Json(result.Message);
            }
        }

        [NoDirectAccess]
        public IActionResult VoucherList(int voucherType)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            // Get Voucher List From Database

            //ResultDto<CompanyFinancialCycle_Dto> result = _companyFacad.GetCompanyFinancialCycleInfoService.Execute(id);
            //if (result.IsSuccess)
            // {
            //  return View(DtosToModels.CompanyFinancialCycleToModel(result.Data));
            return View(new VoucherDialogModel() { Id = 0, voucherType = voucherType, VoucherListModel = new List<VoucherListModel>() { new VoucherListModel() { Id = 5, VoucherDate = "2020-01-01", VoucherNo = "Ys123" }, new VoucherListModel() { Id = 6, VoucherDate = "2020-01-01", VoucherNo = "Ys2222" } } });
            // }
            // else
            //   return Json(result);


        }
        [HttpPost]
        public IActionResult LoadVoucher(long Id, string navType, int voucherType)
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
                resultDto = _navigateJornalVoucherService.Execute(voucherType.ToByte(), Id, navigate_Status);

            if (resultDto.IsSuccess)
            {
                journalVoucherModel = Utilities.DtosToModels.JournalVoucherToModel(resultDto.Data, activeUser.DateFormats_Description, voucherType);
                journalVoucherModel.JournalVoucherContentModel.Add(new JournalVoucherContentModel() { Id = 0, AccountLedger_Id = "0", CostCenter_Id = "0", Currency_Id = "0", DrCr_Id = "0", Type_Id = "0", Amount = 0, Balance = 0, ChequeDate = Convert.ToDateTime("2079-01-01").ToCustomFormat(activeUser.DateFormats_Description), ChequeNo = "0", ExchangeRate = 0, ExchangeRateOld = 0, No = 0, Remark = "", RecStatus = 0 });

            }
            else
            {
                return Json(resultDto);
            }
            ViewBag.PaymentReceiptAccount = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.PaymentReceiptAccountList).Data);
            ViewBag.AccountLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountLedgerList).Data);
            ViewBag.Projects = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.ProjectList).Data);
            ViewBag.Branches = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CompanyBranchList).Data);
            ViewBag.Types = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypeList).Data);
            ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);
            ViewBag.CostCenters = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CostCenterList).Data);

            ViewBag.JurnalVoucher_Id = journalVoucherModel.Id;

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", journalVoucherModel) });

        }
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult VoucherList(long Id, int voucherType)
        {
            return LoadVoucher(Id, null, voucherType);
        }
    }
}
