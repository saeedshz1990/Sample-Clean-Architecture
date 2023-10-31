using Microsoft.AspNetCore.Mvc;
using Sample_Clean_Architecture.Application.Interfaces.FacadPatterns;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyFinancialCycle;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Companies;
using Sample_Clean_Architecture.Web.Utilities;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class CompanyFinancialCycleController : BaseController
    {
        private readonly ICompanyFacad _companyFacad;
        public CompanyFinancialCycleController(ICompanyFacad companyFacad)
        {
            _companyFacad = companyFacad;
        }
        public IActionResult Index(int id)
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ResultDto<CompanyFinancialCycleDto> result = _companyFacad.GetCompanyFinancialCycleServices.Execute(id);
            ViewBag.Company_Id = result.Data.Company_Id;
            return View(DtosToModels.CompanyFinancialCycleToModel(result.Data.CompanyFinancialCycles, activeUser.DateFormats_Description));

        }
        [NoDirectAccess]
        public IActionResult AddOrEdit(int id = 0, int cId = 0)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
            ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();
            ResultDto<CompanyFinancialCycle_Dto> result = _companyFacad.GetCompanyFinancialCycleInfoService.Execute(id);
            if (result.IsSuccess)
            {

                result.Data.Company_Id = cId;
                return View(DtosToModels.CompanyFinancialCycleToModel(result.Data, activeUser.DateFormats_Description));
            }
            else
                return Json(result);
        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, CompanyFinancialCycleModel request)
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewBag.DateFormat = activeUser.DateFormats_Description.ToLower().Replace("yyyy", "yy");
            ViewBag.MaskDateFormat = activeUser.DateFormats_Description.ToLower().Replace("y", "0").Replace("m", "0").Replace("d", "0");
            ViewBag.MaskPlaceHolder = activeUser.DateFormats_Description.ToLower();

            if (ModelState.IsValid)
            {
                ResultDto result = _companyFacad.AddNewCompanyFinancialCycleServices.Execute(Utilities.ModelsToDtos.FinancialCycleToDto(request, activeUser.DateFormats_Description.ToLower()));
                if (result.IsSuccess)
                {

                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CompanyFinancialCycleToModel(_companyFacad.GetCompanyFinancialCycleServices.Execute(request.Company_Id).Data.CompanyFinancialCycles, activeUser.DateFormats_Description)) });
                }
                else
                {
                    return Json(result);
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });
        }
    }
}
