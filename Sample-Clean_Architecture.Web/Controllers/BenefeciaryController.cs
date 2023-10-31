using Microsoft.AspNetCore.Mvc;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.AddNewBenefeciary;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.DeleteBeneficiary;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Queries.GetBenefeciaries;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Benefeciary;
using Sample_Clean_Architecture.Web.Utilities;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class BenefeciaryController : BaseController
    {
        private readonly IGetBenefeciaryService _getBenefeciaryService;
        private readonly IGetBenefeciaryInfoService _getBenefeciaryInfoService;// dependency Injection
        private readonly IAddNewBenefeciaryService _addnewbenefeciaryservice;// dependency Injection
        private readonly IDeleteBeneficiaryService _deleteBeneficiaryService;// dependency Injection



        public BenefeciaryController(IGetBenefeciaryService getBenefeciaryService, IGetBenefeciaryInfoService getBenefeciaryInfoService, IAddNewBenefeciaryService addnewbenefeciaryservice, IDeleteBeneficiaryService deleteBeneficiaryService)
        {
            _getBenefeciaryService = getBenefeciaryService;
            _getBenefeciaryInfoService = getBenefeciaryInfoService;
            _addnewbenefeciaryservice = addnewbenefeciaryservice;
            _deleteBeneficiaryService = deleteBeneficiaryService;
        }
        public IActionResult Index()
        {
            ActiveUser activeUser = CurrentUser.Get();
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ResultDto<List<BenefeciaryList_Dto>> result = _getBenefeciaryService.Execute(activeUser.Company_Id);
            return View(DtosToModels.BenefeciaryToModel(result.Data));
        }

        [NoDirectAccess]
        public IActionResult AddOrEdit(int id = 0)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            if (id > 0)
            {
                ResultDto<Benefeciary_Dto> result = _getBenefeciaryInfoService.Execute(id);
                if (result.IsSuccess)
                {
                    return View(DtosToModels.BenefeciaryInfoToModel(result.Data));
                }
                else
                    return Json(result);
            }
            else
            {
                ActiveUser activeUser = CurrentUser.Get();

                return View(new BenefeciaryInfoModel() { Company_Id = activeUser.Company_Id });
            }
        }

        [HttpPost]
        //[Authorize]

        public IActionResult Delete(int id)
        {

            ResultDto result = _deleteBeneficiaryService.Execute(id);
            if (result.IsSuccess)
            {
                ActiveUser activeUser = CurrentUser.Get();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.BenefeciaryToModel(_getBenefeciaryService.Execute(activeUser.Company_Id).Data)) });
            }
            else
            {
                return Json(result);
            }
        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(BenefeciaryInfoModel request)
        {
            if (ModelState.IsValid)
            {
                ResultDto result = _addnewbenefeciaryservice.Execute(Utilities.ModelsToDtos.BenefeciaryInfoToDto(request));
                if (result.IsSuccess)
                {
                    ActiveUser activeUser = CurrentUser.Get();
                    return Json(new { isValidsp_ExchangeRate_GetById = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.BenefeciaryToModel(_getBenefeciaryService.Execute(activeUser.Company_Id).Data)) });
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
