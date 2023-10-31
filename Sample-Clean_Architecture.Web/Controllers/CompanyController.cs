using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Sample_Clean_Architecture.Application.Interfaces.FacadPatterns;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanies;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Companies;
using Sample_Clean_Architecture.Web.Utilities;
using System.Web;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    [Authorize(Roles = "Creator")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyFacad _companyFacad;
        private readonly IGetListItemService _getListItemService;
        public CompanyController(ICompanyFacad companyFacad, IGetListItemService getListItemService)
        {
            _companyFacad = companyFacad;
            _getListItemService = getListItemService;
        }
        // GET: Company
        public IActionResult Index()
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = CurrentUser.Get();// SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
            int Accounts_Id = activeUser.Accounts_Id;
            return View(DtosToModels.CompanyListToModel(_companyFacad.GetCompanyService.Execute(Accounts_Id).Data));
        }
        [HttpGet]
        // GET: Company/AddOrEdit(Insert)
        // GET: Company/AddOrEdit/5(Update)
        [NoDirectAccess]
        public IActionResult AddOrEdit(int id = 0)
        {
            //ViewBag.Currencies = new SelectList(_companyFacad.GetCurrenciesService.Execute().Data, "Currency_Id", "Currency_Name");
            ViewData["IsRendred"] = CheckIsRendred(Request);


            ResultDto<CompanyGetDto> result = _companyFacad.GetCompanyInfoService.Execute(id);
            if (result.IsSuccess)
            {

                //string currencyLst = (new JavaScriptSerializer()).Serialize(result.Data.Currencies.Select(c => c.Currency_Name));
                //ViewBag.CurrencyLst = HttpUtility.JavaScriptStringEncode(currencyLst,false);

                ViewBag.Countries = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CountryList, result.Data.Countries).Data); //new SelectList(result.Data.Countries, "Country_Id", "Country_Name");


                //  ViewBag.Symbols   = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencyList, result.Data.Currencies).Data);//= new SelectList(result.Data.Currencies, "Currency_Id", "Currency_Symbol");
                ViewBag.CountryCurrencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CountryCurrencyList, result.Data.Countries).Data);//= new SelectList(result.Data.Currencies, "Currency_Id", "Currency_Symbol");

                ViewBag.CurrencySubunits = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencySubunitList, result.Data.Countries).Data);//= new SelectList(result.Data.Currencies, "Currency_Id", "Currency_Symbol");
                string currencySubunitLst = new JavaScriptSerializer().Serialize(result.Data.Countries);
                ViewBag.currencySubunitLst = HttpUtility.JavaScriptStringEncode(currencySubunitLst, false);
                //string symbolLst = (new JavaScriptSerializer()).Serialize(result.Data.Currencies.Select(c => c.Currency_Symbol));
                //ViewBag.SymbolLst = HttpUtility.JavaScriptStringEncode(symbolLst, false);

                ViewBag.DateFormats = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.DateFormatList, result.Data.DateFormats).Data);// new SelectList(result.Data.DateFormats, "DateFormats_Id", "DateFormats_Description");

                ViewBag.DefaultLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.DefaultLedgerList, result.Data.DefaultLedgers).Data);//= new SelectList(result.Data.DefaultLedgers, "Id", "Title");

                return View(DtosToModels.CompanyInfoToModel(result.Data.RequestCompany, result.Data.DateFormats.First(c => c.DateFormats_Id == result.Data.RequestCompany.DateFormats_Id).DateFormats_Description.Replace(" ", "/").Replace("YYYY", "yyyy").Replace("DD", "dd")));
            }
            else
                return View(new CompanyModel());
            // }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, CompanyModel request)
        {
            List<ListItemDto> listItems = _getListItemService.Execute(Common.Enums.ListType.DateFormatList).Data;
            ViewBag.DateFormats = DropDownList.GetSelectListItems(listItems);// new SelectList(result.Data.DateFormats, "DateFormats_Id", "DateFormats_Description");
            ViewBag.Countries = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CountryList).Data); //new SelectList(result.Data.Countries, "Country_Id", "Country_Name");
            ViewBag.CountryCurrencies = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CountryCurrencyList).Data);//= new SelectList(result.Data.Currencies, "Currency_Id", "Currency_Symbol");
            ViewBag.DefaultLedgers = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.DefaultLedgerList).Data);//= new SelectList(result.Data.DefaultLedgers, "Id", "Title");
            ViewBag.CurrencySubunits = DropDownList.GetSelectListItems(_getListItemService.Execute(Common.Enums.ListType.CurrencySubunitList).Data);//= new SelectList(result.Data.Currencies, "Currency_Id", "Currency_Symbol");

            if (ModelState.IsValid)
            {
                ActiveUser activeUser = CurrentUser.Get(); // SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
                request.Accounts_Id = activeUser.Accounts_Id;

                ResultDto result = _companyFacad.AddNewCompanyService.Execute(Utilities.ModelsToDtos.CompanyToDto(request, listItems.First(c => c.Id == request.DateFormats_Id).Description.Replace(" ", "/").Replace("YYYY", "yyyy").Replace("DD", "dd").Replace("MM", "mm")));
                if (result.IsSuccess)
                {

                    int Accounts_Id = activeUser.Accounts_Id;
                    // return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CompanyListToModel(_companyFacad.GetCompanyService.Execute(Accounts_Id).Data)) });
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

        // GET: Company/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel =  /*await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);*/
               new CompanyModel() { CompanyId = 1 };

            if (companyModel == null)
            {
                return NotFound();
            }

            return View(companyModel);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //Delete From Database

            /* var transactionModel = await _context.Transactions.FindAsync(id);
             _context.Transactions.Remove(transactionModel);
             await _context.SaveChangesAsync();*/
            int Accounts_Id = 1;
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CompanyListToModel(_companyFacad.GetCompanyService.Execute(Accounts_Id).Data)) });
        }
        [HttpPost, ActionName("Close")]
        public IActionResult CloseDialog()
        {
            ActiveUser activeUser = CurrentUser.Get();
            int Accounts_Id = activeUser.Accounts_Id;

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CompanyListToModel(_companyFacad.GetCompanyService.Execute(Accounts_Id).Data)) });
        }
        [HttpGet]
        public JsonResult Test()
        {
            var name = HttpContext.Request.Query["term"].ToString();
            var jobnames = new List<CurrencyDto>() {
            new CurrencyDto(){ Currency_Id = 1, Currency_Name="Afghanistan"},
            new CurrencyDto(){ Currency_Id = 2, Currency_Name="Albania"},
            new CurrencyDto(){ Currency_Id = 3, Currency_Name="Algeria"},
            new CurrencyDto(){ Currency_Id = 4, Currency_Name="AmericanSamoa"},
            new CurrencyDto(){ Currency_Id = 5, Currency_Name="Andorra"},
            new CurrencyDto(){ Currency_Id = 6, Currency_Name="Angola"},
            new CurrencyDto(){ Currency_Id = 6, Currency_Name="Anguilla"},
            new CurrencyDto(){ Currency_Id = 6, Currency_Name="Argentina"},
            new CurrencyDto(){ Currency_Id = 6, Currency_Name="Armenia"},
        };
            var data = jobnames.Where(j => j.Currency_Name.Contains(name)).Select(j => j.Currency_Name).ToList();
            return Json(data);
        }
        private bool CompanyModelExists(int id)
        {
            return true;
            // return _context.Transactions.Any(e => e.TransactionId == id);
        }

    }
}
