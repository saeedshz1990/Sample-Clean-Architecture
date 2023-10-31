using Microsoft.AspNetCore.Mvc;
using PasswordGenerator;
using Sample_Clean_Architecture.Application.Interfaces.FacadPatterns;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyUser;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyUsers;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserChange;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserLogin;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Companies;
using Sample_Clean_Architecture.Web.Utilities;
using static Sample_Clean_Architecture.Web.Helper;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class CompanyUsersController : BaseController
    {
        private readonly IUserLoginService _userLoginService;
        private readonly ICompanyFacad _companyFacad;
        private readonly IMessageSender _messageSender;
        private readonly IUserChangeService _userChangeService;

        public CompanyUsersController(
            IUserLoginService userLoginService, 
            ICompanyFacad companyFacad, 
            IMessageSender messageSender, 
            IUserChangeService userChangeService)
        {
            _userLoginService = userLoginService;
            _companyFacad = companyFacad;
            _messageSender = messageSender;
            _userChangeService = userChangeService;
        }

        public IActionResult Index(int id)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);
            ResultDto<CompanyUsersDto> result = _companyFacad.GetCompanyUsersServices.Execute(id);
            ViewBag.Company_Id = result.Data.Company_Id;
            return View(DtosToModels.CompanyUserToModel(result.Data.CompanyUsers));

        }
        [NoDirectAccess]
        public IActionResult AddOrEdit(int id = 0, int cId = 0)
        {
            ViewData["IsRendred"] = CheckIsRendred(Request);

            ResultDto<CompanyUserDto> result = _companyFacad.GetCompanyUserInfoService.Execute(id);
            if (result.IsSuccess)
            {

                result.Data.Company_Id = cId;
                return View(DtosToModels.CompanyUserToModel(result.Data));
            }
            else
                return Json(result);


        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, CompanyUserModel request)
        {
            if (ModelState.IsValid)
            {
                var pwd = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial().LengthRequired(8);
                var passresult = pwd.Next();
                string passHashed = Cryptography.Encrypt(passresult, "DumiSoft.Com");
                request.Password = passHashed;
                ResultDto<ResultCompanyUserDto> resultDto = _companyFacad.AddNewCompanyUserServices.Execute(ModelsToDtos.CompanyUserToDto(request));
                if (resultDto.IsSuccess)
                {
                    if (resultDto.Message == AppMessages.USER_REGISTER_SUCCESS && resultDto.Data.StatusOpr == 1)
                    {
                        /*
                        string uname = resultDto.Data.CompanyUsers_Id.ToString() + "_" + resultDto.Data.Users_Id.ToString();
                        var emailConfirmationToken = Cryptography.Encrypt(uname, "DumiSoft.Com");
                        var emailMessage = "Your Password :" + passresult + "</br>" + "Please Verify Your Email Before Login " + "</br>" +
                            Url.Action("ConfirmEmail", "Auth",
                                new { token = emailConfirmationToken },
                                Request.Scheme);
                        _messageSender.SendEmailAsync(request.UserName, "Email confirmation", emailMessage);
                        */
                    }

                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CompanyUserToModel(_companyFacad.GetCompanyUsersServices.Execute(request.Company_Id).Data.CompanyUsers)) });
                }
                else
                {
                    return Json(resultDto);
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", request) });
        }


        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id, int cId)
        {
            //Delete From Database
            ResultDto result = _companyFacad.DeleteCompanyUserService.Execute(id);
            if (result.IsSuccess)
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", DtosToModels.CompanyUserToModel(_companyFacad.GetCompanyUsersServices.Execute(cId).Data.CompanyUsers)) });
            else
                return Json(result);

        }

        [HttpPost]
        public IActionResult SetUserInformation(int CompanyUsers_Id, string returnUrl)
        {
            ActiveUser activeUser = CurrentUser.Get();
            var signupResult = _userChangeService.Execute(CompanyUsers_Id);
            if (signupResult.IsSuccess == true)
            {
                activeUser.Menus = signupResult.Data.Menus;
                activeUser.DateFormats_Id = signupResult.Data.DateFormats_Id;
                activeUser.Company_DateSeperator = signupResult.Data.Company_DateSeperator;
                activeUser.Company_TransactionType = signupResult.Data.Company_TransactionType;
                activeUser.FinancialCycle_FromDate = signupResult.Data.FinancialCycle_FromDate;
                activeUser.FinancialCycle_ToDate = signupResult.Data.FinancialCycle_ToDate;
                activeUser.CompanyUsers_Id = CompanyUsers_Id;
                activeUser.Company_Id = signupResult.Data.Company_Id;
                CurrentUser.Set(activeUser);
            }
            return LocalRedirect(returnUrl);
        }
    }
}
