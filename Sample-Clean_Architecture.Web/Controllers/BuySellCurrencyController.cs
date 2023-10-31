using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.GetJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.NavigateJornalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.GetRemittanceForInsert;
using Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.LoadRemittance;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Rimittance;
using Sample_Clean_Architecture.Web.Models.Voucher;
using Sample_Clean_Architecture.Web.Utilities;
using System.Web;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class BuySellCurrencyController : BaseController
    {
        private readonly IGetListItemService _getListItemService;
        private readonly ILoadRemittanceService _loadRemittanceService;

        private readonly IAddNewJournalVoucherSevice _addNewJournalVoucherSevice;
        private readonly IGetJournalVoucher _getJournalVoucher;



        private readonly IGetRemittanceForInsertService _getRemittanceForInsertService;


        private readonly INavigateJornalVoucherService _navigateJornalVoucherService;


        public BuySellCurrencyController(IGetListItemService getListItemService, IAddNewJournalVoucherSevice addNewJournalVoucherSevice, ILoadRemittanceService loadRemittanceService, IGetRemittanceForInsertService getRemittanceForInsertService, IGetJournalVoucher getJournalVoucher, INavigateJornalVoucherService navigateJornalVoucherService)
        {
            _getListItemService = getListItemService;
            _getRemittanceForInsertService = getRemittanceForInsertService;
            _addNewJournalVoucherSevice = addNewJournalVoucherSevice;
            _loadRemittanceService = loadRemittanceService;
            _navigateJornalVoucherService = navigateJornalVoucherService;
            _getJournalVoucher = getJournalVoucher;
        }

        [HttpGet("BuySellCurrency/Index/{voucherType}")]
        public IActionResult Index(int voucherType)//Sell Curreny:63 BuyCurrency:65
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = CurrentUser.Get();


            ResultDto<RemittanceLoadDto> result = _loadRemittanceService.Execute(activeUser.Company_Id, voucherType, activeUser.IsCurrentDate, activeUser.WorkDay);

            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
            ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();

            BuySellCurrencyModel buySellCurrencyModel = new BuySellCurrencyModel();


            if (result.IsSuccess)
            {
                buySellCurrencyModel.InvoiceNo = result.Data.VoucherNumber;
                buySellCurrencyModel.VoucherDate = result.Data.VoucherDate.ToCustomFormat(activeUser.DateFormats_Description);
                buySellCurrencyModel.JournalVoucherContentModel = new List<JournalVoucherContentModel>();
                buySellCurrencyModel.JournalVoucherContentModel.Add(new JournalVoucherContentModel() { Id = 0, AccountLedger_Id = "0", CostCenter_Id = "0", Currency_Id = "0", DrCr_Id = "0", Type_Id = "0", Amount = 0, Balance = 0, ChequeDate = Convert.ToDateTime("2079-01-01").ToCustomFormat(activeUser.DateFormats_Description), ChequeNo = "0", ExchangeRate = 0, ExchangeRateOld = 0, No = 0, Remark = "", RecStatus = 0 });
                buySellCurrencyModel.AvalableRateContentModel = new List<AvalableRateContentModel>();
                buySellCurrencyModel.VoucherType = voucherType;

                buySellCurrencyModel.Remittance_AmountDecimalNo = result.Data.RemittanceInfo.Remittance_AmountDecimalNo;
                buySellCurrencyModel.Remittance_BalanceDifference = result.Data.RemittanceInfo.Remittance_BalanceDifference;
                buySellCurrencyModel.Remittance_BalanceDifferenceLedger = result.Data.RemittanceInfo.Remittance_BalanceDifferenceLedger;
                buySellCurrencyModel.Remittance_RateDecimalNo = result.Data.RemittanceInfo.Remittance_RateDecimalNo;


                //for test
                // ResultDto<List<RemittanceCurrenciesDto>> resultTest = _getRemittanceForInsertService.Execute(activeUser.Company_Id, 4);
                // buySellCurrencyModel.AvalableRateContentModel = Utilities.DtosToModels.AvalableRateContentToModel(resultTest.Data);

                buySellCurrencyModel.AvalableRatesContentModel = Utilities.DtosToModels.AvalableRatesContentToModel(result.Data.RemittanceAllCurrencies);
            }
            ViewBag.AccountLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountLedgerList, result.Data.AccountLedgerList).Data);
            ViewBag.CustomerAccountList = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.PaymentReceiptAccountList, result.Data.CustomerAccountList).Data);
            ViewBag.Types = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypeList, result.Data.VoucherTypeList).Data);

            string currenciesLst = new JavaScriptSerializer().Serialize(result.Data.CurrencyCompanyList);
            ViewBag.CurrenciesLst = HttpUtility.JavaScriptStringEncode(currenciesLst, false);

            ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList, result.Data.CurrencyCompanyList).Data, result.Data.DefaultCurrency_Id);
            result.Data.CurrencyCompanyList.Remove(result.Data.CurrencyCompanyList.Find(c => c.Currency_Id == result.Data.DefaultCurrency_Id));
            ViewBag.CurrenciesWithoutDefault = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyListWithoutDefault, result.Data.CurrencyCompanyList).Data);
            ViewBag.voucherMasters_Id = 0;
            buySellCurrencyModel.BuyingExchangeRate = _getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyListWithoutDefault).Data[0].Tag.ToDecimal();
            buySellCurrencyModel.SellingExchangeRate = _getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data.FirstOrDefault(c => c.Id == result.Data.DefaultCurrency_Id).Tag.ToDecimal();

            ViewBag.AmountDecimalNo = result.Data.RemittanceInfo.Remittance_AmountDecimalNo;
            ViewBag.AmountDecimalNo = result.Data.RemittanceInfo.Remittance_RateDecimalNo;

            return View(buySellCurrencyModel);
        }
        [HttpPost]
        public IActionResult Index(BuySellCurrencyModel model)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            string lstDrCr_Id = "1";
            if (model.VoucherType == 63)
            {
                lstDrCr_Id = "2";

            }
            model.JournalVoucherContentModel.RemoveAll(c => c.AccountLedger_Id == "0");
            model.JournalVoucherContentModel.ForEach(c => c.DrCr_Id = lstDrCr_Id);
            model.JournalVoucherContentModel.ForEach(c => c.ChequeDate = "2079-01-01");

            model.JournalVoucherContentModel.Insert(0, new JournalVoucherContentModel() { AccountLedger_Id = model.LedgerAccount_Id.ToString(), Currency_Id = model.BuyingCurrency_Id.ToString(), ExchangeRate = model.BuyingExchangeRate, DrCr_Id = model.VoucherType == 63 ? "1" : "2", Amount = model.BuyingAmount, Remark = model.BuyingRemark, ExchangeRateOld = model.BuyingExchangeRateOld, ChequeDate = "2079-01-01" });
            model.JournalVoucherContentModel.Insert(1, new JournalVoucherContentModel() { AccountLedger_Id = model.LedgerAccount_Id.ToString(), Currency_Id = model.SellingCurrency_Id.ToString(), ExchangeRate = model.SellingExchangeRate, DrCr_Id = model.VoucherType == 63 ? "1" : "2", Amount = model.SellingAmount, Remark = model.SellingRemark, ExchangeRateOld = model.SellingExchangeRateOld, ChequeDate = "2079-01-01" });

            ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
            JournalVoucherDto journalVoucherDto = Utilities.ModelsToDtos.JournalVoucherToDto(model, activeUser.DateFormats_Description.ToLower());

            List<AvalableRateContentModel> avalableRateContentDto = new List<AvalableRateContentModel>();
            foreach (AvalableRateContentModel avalableRateContentModel in model.AvalableRateContentModel)
            {
                avalableRateContentDto.Add(new AvalableRateContentModel()
                {
                    RemittanceSell_Amount = avalableRateContentModel.RemittanceSell_Amount,
                    RemittanceSell_Id = avalableRateContentModel.RemittanceSell_Id,
                    RemmitenceBatch_Id = avalableRateContentModel.RemmitenceBatch_Id,
                    RecStatus = avalableRateContentModel.RecStatus //getRecStatus(avalableRateContentModel)
                });
            }
            ResultDto<int> result = _addNewJournalVoucherSevice.Execute(new RequestJournalVoucher()
            {
                MasterInfo = JsonConvert.SerializeObject(journalVoucherDto.JournalVoucherMasterDto),
                DetailInfo = JsonConvert.SerializeObject(journalVoucherDto.JournalVoucherDetailsDto),
                Company_Id = activeUser.Company_Id,
                Users_Id = activeUser.Users_Id,
                voucherType_Id = model.VoucherType.ToByte(),
                SecondRate = model.TMN_AED,
                CurrencyRateStr = JsonConvert.SerializeObject(avalableRateContentDto),

            });
            if (result.IsSuccess)
            {

                ViewBag.AccountLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountLedgerList).Data);
                ViewBag.CustomerAccountList = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.PaymentReceiptAccountList).Data);
                ViewBag.Types = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypeList).Data);
                ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);
                ViewBag.CurrenciesWithoutDefault = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyListWithoutDefault).Data);

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

        private byte getRecStatus(AvalableRateContentModel avalableRateContent)
        {
            byte recStatus = 0;
            if (avalableRateContent.RemittanceSell_Amount != avalableRateContent.RemittanceSell_OldAmount)
            {
                if (avalableRateContent.RemittanceSell_Amount == 0)
                    recStatus = 2;
                else
                    recStatus = 1;
            }

            return recStatus;
        }
        [HttpPost]
        public IActionResult GetRemittanceCurrencies(long currencyId)
        {
            ActiveUser activeUser = CurrentUser.Get();
            ResultDto<List<RemittanceCurrenciesDto>> result = _getRemittanceForInsertService.Execute(activeUser.Company_Id, currencyId);
            if (result.IsSuccess)
            {
                string rate = _getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyListWithoutDefault).Data.FirstOrDefault(c => c.Id == currencyId).Tag;
                RemittanceCurrencies_Dto resultDto = new RemittanceCurrencies_Dto() { Rate = rate.ToFloat(), RemittanceCurrenciesDto = result.Data };
                return Json(resultDto);
            }
            else
            {
                return Json(result.Message);
            }
        }
        [HttpPost]
        public IActionResult LoadVoucher(long Id, string navType, byte voucherType)
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
            BuySellCurrencyModel buySellCurrencyModel = new BuySellCurrencyModel();
            ResultDto<JournalVoucherDto> resultDto;
            if (navigate_Status == 10)
                resultDto = _getJournalVoucher.Execute(Id);
            else
                resultDto = _navigateJornalVoucherService.Execute(voucherType, Id, navigate_Status);

            if (resultDto.IsSuccess)
            {
                buySellCurrencyModel = Utilities.DtosToModels.JournalVoucherToBuySellCurrencyModel(resultDto.Data, activeUser.DateFormats_Description, voucherType);
                buySellCurrencyModel.JournalVoucherContentModel.Add(new JournalVoucherContentModel() { Id = 0, AccountLedger_Id = "0", Currency_Id = "0", DrCr_Id = "0", Type_Id = "0", Amount = 0, Balance = 0, ExchangeRate = 0, ExchangeRateOld = 0, No = 0, Remark = "", RecStatus = 0 });

            }
            else
            {
                return Json(resultDto);
            }
            ViewBag.AccountLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.AccountLedgerList).Data);
            ViewBag.CustomerAccountList = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.PaymentReceiptAccountList).Data);
            ViewBag.Types = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.VoucherTypeList).Data);
            ViewBag.Currencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyList).Data);
            ViewBag.CurrenciesWithoutDefault = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyCompanyListWithoutDefault).Data);


            ViewBag.voucherMasters_Id = buySellCurrencyModel.Id;

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", buySellCurrencyModel) });

        }
    }
}
