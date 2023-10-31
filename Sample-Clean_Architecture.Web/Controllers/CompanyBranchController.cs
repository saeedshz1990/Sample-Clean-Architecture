using Microsoft.AspNetCore.Mvc;
using Sample_Clean_Architecture.Application.Interfaces.FacadPatterns;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyBranches;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Companies;
using Sample_Clean_Architecture.Web.Utilities;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class CompanyBranchController : BaseController
    {
        private readonly ICompanyFacad _companyFacad;
        public CompanyBranchController(ICompanyFacad companyFacad)
        {
            _companyFacad = companyFacad;
        }
        public IActionResult Index(int id)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ResultDto<CompanyBranchDto> result = _companyFacad.GetCompanyBranchServices.Execute(id);
            ViewBag.Company_Id = result.Data.Company_Id;
            return View(DtosToModels.CompanyBranchToModel(result.Data.CompanyBranches));

        }
        [NoDirectAccess]
        public IActionResult AddOrEdit(int id = 0, int cId = 0)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ViewBag.Company_Id = cId;
            ResultDto<CompanyBranch_Dto> result = _companyFacad.GetCompanyBranchInfoService.Execute(id);
            if (result.IsSuccess)
            {

                result.Data.Company_Id = cId;
                return View(DtosToModels.CompanyBranchToModel(result.Data));
            }
            else
                return Json(result);


        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, CompanyBranchModel request)
        {
            if (ModelState.IsValid)
            {
                ResultDto result = _companyFacad.AddNewCompanyBranchServices.Execute(Utilities.ModelsToDtos.CompanyBranchToDto(request));
                if (result.IsSuccess)
                {

                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CompanyBranchToModel(_companyFacad.GetCompanyBranchServices.Execute(request.Company_Id).Data.CompanyBranches)) });
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
