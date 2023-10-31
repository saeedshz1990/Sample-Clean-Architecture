using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PasswordGenerator;
using Sample_Clean_Architecture.Application.Services.Common.Commands.UserProfile;
using Sample_Clean_Architecture.Application.Services.MemoryCash;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserActiveAccount;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserForgot;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserLogin;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserRegister;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Web.Models.Auth;
using Sample_Clean_Architecture.Web.Utilities;
using System.Security.Claims;

namespace Sample_Clean_Architecture.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUserLoginService _userLoginService;
        private readonly IUserForgotService _userForgotService;

        private readonly IUserUpdatePassService _userUpdatePassService;

        private readonly IUserRegisterService _userRegisterService;
        private readonly IUserActiveAccountService _userActiveAccountService;
        private readonly IMessageSender _messageSender;
        private IFlushableMemoryCache _memoryCache;

        private readonly IUserProfileService _userProfileService;
        private readonly CookiesManeger cookiesManeger;
        public AuthController(
            IUserLoginService userLoginService, 
            IUserRegisterService userRegisterService,
            IUserActiveAccountService userActiveAccountService, 
            IMessageSender messageSender, 
            IFlushableMemoryCache memoryCache, 
            IUserProfileService userProfileService, 
            IUserForgotService userForgotService, 
            IUserUpdatePassService userUpdatePassService)
        {
            _userLoginService = userLoginService;
            _userRegisterService = userRegisterService;
            _userActiveAccountService = userActiveAccountService;
            _messageSender = messageSender;
            _memoryCache = memoryCache;
            _userProfileService = userProfileService;
            _userForgotService = userForgotService;
            _userUpdatePassService = userUpdatePassService;
            cookiesManeger = new CookiesManeger();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Login(RegisterMessageModel Messagemodel)
        {
            var pwd = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial("@$%#!*&").LengthRequired(8);
            var passresult = pwd.Next();
            /*if (Company != null && Company.Length > 0)
            {
                ViewData["Company"] = Company;
                return View();
            }
            else
            {
                Company = cookiesManeger.GetValue(HttpContext, "Company");
                if (Company != null)
                    return Redirect("/Login/" + Company);
                else
                    return RedirectToAction("Register", "Auth");
            }*/
            if (Messagemodel.Message != null && Messagemodel.Message.Length > 0)
            {
                ViewData["Message"] = Messagemodel.Message;

            }
            return View();
        }
        [HttpPost]

        public IActionResult Login(LoginModel viewModel, string returnUrl = "")
        {
            var signupResult = _userLoginService.Execute(viewModel.UserName, viewModel.Password);
            if (signupResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signupResult.Data.Users_Id.ToString()),
              //  new Claim(ClaimTypes.Email, signupResult.Data.Email),
                new Claim(ClaimTypes.Name, signupResult.Data.Accounts_Id.ToString()),
            };
                // claims.Add(new Claim(type: "OrgEmp_Id", value: signupResult.Data.OrgEmp_Id.ToString()));


                foreach (var item in signupResult.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);

                var activeUser = new ActiveUser()
                {
                    Accounts_Id = signupResult.Data.Accounts_Id,
                    Users_Id = signupResult.Data.Users_Id,
                    Roles = signupResult.Data.Roles,
                    Menus = signupResult.Data.Menus,
                    DateFormats_Id = signupResult.Data.DateFormats_Id,
                    UserInBusiness = signupResult.Data.UserInBusiness,
                    Company_DateSeperator = signupResult.Data.Company_DateSeperator,
                    Company_TransactionType = signupResult.Data.Company_TransactionType,
                    FinancialCycle_FromDate = signupResult.Data.FinancialCycle_FromDate,
                    FinancialCycle_ToDate = signupResult.Data.FinancialCycle_ToDate,
                    UserName = viewModel.UserName,
                    Password = Cryptography.Encrypt(viewModel.Password, "DumiSoft.Com"),
                    Company_Id = signupResult.Data.Company_Id,
                    UserProfile = signupResult.Data.UserProfile,
                    IsCurrentDate = true,
                    WorkDay = DateTime.Now,
                    DateFormats_Description = signupResult.Data.DateFormats_Description.Replace("YYYY", "yyyy").Replace("DD", "dd") //"yyyy/MM/dd"

                };

                //Set Default User Business
                if (signupResult.Data.UserInBusiness != null && signupResult.Data.UserInBusiness.Count > 0)
                    activeUser.CompanyUsers_Id = signupResult.Data.UserInBusiness[0].CompanyUsers_Id;

                //SessionExtension.SetObject( HttpContext.Session,"ActiveUser", activeUser);
                CurrentUser.Set(activeUser);


                /*   var claimsIdentity = User.Identity as ClaimsIdentity;
                   int OrgEmp_Id = Convert.ToInt32(claimsIdentity.Claims.Where(p => p.Type == "OrgEmp_Id").First().Value);*/
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Dashboard", "Home");

            }
            return View();
        }
        [HttpGet]
        public IActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Forgot(ForgotModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ResultDto<ResultUserForgotDto> forgotResult = _userForgotService.Execute(viewModel.Email);
                if (forgotResult.IsSuccess == true)
                {
                    var pwd = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial("@$%#!*&").LengthRequired(8);
                    var passresult = pwd.Next();
                    string passHashed = Cryptography.Encrypt(passresult, "DumiSoft.Com");
                    ResultDto result = _userUpdatePassService.Execute(forgotResult.Data.Users_Id, passHashed);
                    if (result.IsSuccess == true)
                    {
                        SendEmail(forgotResult.Data.Accounts_Id, forgotResult.Data.Users_Id, viewModel.Email, passresult, false);
                        ViewData["Message"] = "New password send to your email .";

                    }
                }
                else
                {
                    if (forgotResult.Message == AppMessages.USER_NOT_FOUND)
                    {
                        @ViewData["Message"] = AppMessages.USER_NOT_FOUND;
                    }
                    else if (forgotResult.Message == AppMessages.USER_NOT_ACTIVATED)
                    {
                        var pwd = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial("@$%#!*&").LengthRequired(8);
                        var passresult = pwd.Next();
                        SendEmail(forgotResult.Data.Accounts_Id, forgotResult.Data.Users_Id, viewModel.Email, passresult, true);
                        ViewData["Message"] = "We've emailed you a confirmation link and new password . ";

                    }

                }
            }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        private void SendEmail(long accounts_Id, int users_Id, string email, string passresult, bool isConfirm)
        {

            string uname = accounts_Id.ToString() + "_" + users_Id.ToString();
            var emailConfirmationToken = Cryptography.Encrypt(uname, "DumiSoft.Com");
            var emailMessage = "";
            if (!isConfirm)
            {
                emailMessage = "<html><body> <div style=\"font-size:medium;float:left;direction:ltr;padding-top:50px\">" + "Hi <span style=\"font-weight: bold\" >" + email + "</span> <br/>" + " Your Password :<span style=\"font-weight: bold\" >" + passresult + "</span><br/>" +

            "<div >" + "</body></html>";
            }
            else
            {
                emailMessage = "<html><body> <div style=\"font-size:medium;float:left;direction:ltr;padding-top:50px\">" + "Hi <span style=\"font-weight: bold\" >" + email + "</span> <br/>" + " Your Password :<span style=\"font-weight: bold\" >" + passresult + "</span><br/>" + "Please Verify Your Email Before Login " + "<br/> <a href=\" " +
                   Url.Action("ConfirmEmail", "Auth",
                       new { token = emailConfirmationToken },
                       Request.Scheme) + "\" > Confirm Now </a>"
               + "<div >" + "</body></html>";
            }
            _messageSender.SendEmailAsync(email, "Dumi Soft Email Confirmation", emailMessage, true);

        }
        [HttpPost]
        public IActionResult Register(RegisterModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //var pwd = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial().LengthRequired(8);
                //var passresult = pwd.Next();

                var pwd = new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial("@$%#!*&").LengthRequired(8);
                var passresult = pwd.Next();
                string passHashed = Cryptography.Encrypt(passresult, "DumiSoft.Com");
                ResultDto<ResultUserRegisterDto> resultDto = _userRegisterService.Execute(new RequestUserRegisterDto() { Email = viewModel.Email, Password = passHashed, Name = viewModel.Name });
                if (resultDto.IsSuccess == true)
                {
                    if (resultDto.Message == AppMessages.USER_REGISTER_SUCCESS)
                    {
                        SendEmail(resultDto.Data.Accounts_Id, resultDto.Data.Users_Id, viewModel.Email, passresult, true);
                        
                        ViewData["Message"] = "We've emailed you a confirmation link. Once you confirm your email you can continue setting up your profile.";
                    }
                    else if (resultDto.Message == AppMessages.USER_EXISTS)
                    {
                        ViewData["Message"] = AppMessages.USER_EXISTS;
                    }
                }
            }

            return View(viewModel);
        }

        [HttpGet]

        public IActionResult ConfirmEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
                return NotFound();

            string str = Cryptography.Decrypt(token, "DumiSoft.Com");
            string[] result = str.Split('_');
            int accountId = Convert.ToInt32(result[0]);
            int userId = Convert.ToInt32(result[1]);
            //Custom Code And Get accountId And userId
            // var user = await _userManager.FindByNameAsync(userName);
            // if (user == null) return NotFound();
            ResultDto<int> resultDto = _userActiveAccountService.Execute(new RequestUserActiveAccountDto() { AccountId = accountId, UserId = userId });
            RegisterMessageModel registerMessageModel = new RegisterMessageModel() { Message = resultDto.IsSuccess ? "Email Confirmed" : "Email Not Confirmed" };
            // return Content(resultDto.IsSuccess ? "Email Confirmed" : "Email Not Confirmed");
            return RedirectToAction("Login", "Auth", registerMessageModel);
        }
        public IActionResult SignOut()
        {
            _memoryCache.Reset();

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Auth");
        }
        public IActionResult SetUserProfile(int insertKind, string value)
        {
            UserProfileDto dto = new UserProfileDto();
            dto.InsertKind = insertKind.ToByte();
            ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");
            dto.Users_Id = activeUser.Users_Id;

            switch (insertKind)
            {
                case 1:
                    dto.UsersProfile_isFullscreen = value.ToBool();
                    break;
                case 2:
                    dto.UsersProfile_TemplateKind = value.ToByte();
                    break;
                case 3:
                    dto.UsersProfile_isLight = value.ToBool();
                    break;
            }

            ResultDto result = _userProfileService.Execute(dto);
            return Json(result.Message);
        }
        public IActionResult GetUserProfile()
        {

            ActiveUser activeUser = SessionExtension.GetObject<ActiveUser>(HttpContext.Session, "ActiveUser");

            return Json(activeUser.UserProfile);
        }

    }
}
