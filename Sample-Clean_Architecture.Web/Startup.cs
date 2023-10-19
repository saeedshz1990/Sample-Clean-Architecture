using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Options;
using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Interfaces.FacadPatterns;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountGroup;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountLedger;
using Sample_Clean_Architecture.Application.Services.Account.Commands.DeleteAccountGroup;
using Sample_Clean_Architecture.Application.Services.Account.Commands.DeleteAccountLedger;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccess;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccount;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccountGroup;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetNature;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.AddNewBenefeciary;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.DeleteBeneficiary;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Queries.GetBenefeciaries;
using Sample_Clean_Architecture.Application.Services.Common.Commands.UserProfile;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetCurrentDate;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem;
using Sample_Clean_Architecture.Application.Services.Companies.FacadPattern;
using Sample_Clean_Architecture.Application.Services.CostCenter.Commands.AddNewCostCenter;
using Sample_Clean_Architecture.Application.Services.CostCenter.DeleteCostCenter;
using Sample_Clean_Architecture.Application.Services.CostCenter.Queries.GetCostCenters;
using Sample_Clean_Architecture.Application.Services.Currencies.Commands.AddNewCurrency;
using Sample_Clean_Architecture.Application.Services.Currencies.Commands.DeleteCurrency;
using Sample_Clean_Architecture.Application.Services.Currencies.Queries.GetCurrencies;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Commands.AddNewExchangeRate;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Commands.DeleteExchangeRate;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Queries.GetExchangeRate;
using Sample_Clean_Architecture.Application.Services.MemoryCash;
using Sample_Clean_Architecture.Application.Services.Project.Commands.AddNewProject;
using Sample_Clean_Architecture.Application.Services.Project.Commands.DeleteProject;
using Sample_Clean_Architecture.Application.Services.Project.Queries.GetProjects;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Commands.AddNewSuffixPrefix;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.LoadSuffixPrefix;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserAccess;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserActiveAccount;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserBranchAccess;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserChange;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserForgot;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserLogin;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserRegister;
using Sample_Clean_Architecture.Application.Services.Users.Queries.GetUserAccesses;
using Sample_Clean_Architecture.Application.Services.Users.Queries.GetUserBranchAccess;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.GetJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.NavigateJornalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.PaymentVoucher.Queries.LoadPaymentlVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.ReceiptVoucher.Queries.LoadReceiptVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.GetRemittanceForInsert;
using Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.LoadRemittance;
using Sample_Clean_Architecture.Common.Roles;
using System.Globalization;

namespace Sample_Clean_Architecture.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMemoryCache();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new CustomMetadataProvider()));


            services.AddMemoryCache();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new List<CultureInfo> {
        new CultureInfo("en"),
        new CultureInfo("fr")
                };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRoles.Creator, policy => policy.RequireRole(UserRoles.Creator));
                options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Auth/Login");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20.0);
                options.AccessDeniedPath = new PathString("/Auth/Login");
            });

            services.AddScoped<IDatabaseContext, DatabaseContext>();

            //فساد تعریف گردد
            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddScoped<IUserRegisterService, UserRegisterService>();
            services.AddScoped<IUserChangeService, UserChangeService>();
            services.AddScoped<IUserActiveAccountService, UserActiveAccountService>();
            services.AddScoped<IUserForgotService, UserForgotService>();
            services.AddScoped<IUserUpdatePassService, UserUpdatePassService>();

            services.AddScoped<IMessageSender, MessageSender>();
            services.AddScoped<IUserAccessService, UserAccessService>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            //فساد تعریف گردد

            services.AddScoped<IGetAccountGroupService, GetAccountGroupService>();
            services.AddScoped<IAddNewAccountGroupService, AddNewAccountGroupService>();
            services.AddScoped<IGetAccountGroupInfoService, GetAccountGroupInfoService>();
            services.AddScoped<IDeleteAccountGroupService, DeleteAccountGroupService>();
            services.AddScoped<IGetNatureService, GetNatureService>();
            services.AddScoped<IGetAccessService, GetAccessService>();

            services.AddScoped<IGetListItemService, GetListItemService>();
            services.AddScoped<IFlushableMemoryCache, FlushableMemoryCache>();

            services.AddScoped<IGetAccountService, GetAccountService>();
            services.AddScoped<IGetAccountInfoService, GetAccountInfoService>();
            services.AddScoped<IAddNewAccountLedgerService, AddNewAccountLedgerService>();
            //Cost center
            services.AddScoped<IGetCostCenterService, GetCostCenterService>();
            services.AddScoped<IAddNewCostCenterService, AddNewCostCenterService>();
            //services.AddScoped<IListCostCenterService, ListCostCenterService>();
            services.AddScoped<IGetCostCenterInfoService, GetCostCenterInfoService>();
            services.AddScoped<IDeleteCostCenterService, DeleteCostCenterService>();

            //Cost center
            services.AddScoped<IGetProjectService, GetProjectService>();
            services.AddScoped<IAddNewProjectService, AddNewProjectService>();
            services.AddScoped<IGetProjectInfoService, GetProjectInfoService>();
            services.AddScoped<IDeleteProjectService, DeleteProjectService>();

            //Facade Inject
            services.AddScoped<ICompanyFacad, CompanyFacad>();

            //فساد دسترسی منوها
            services.AddScoped<IGetUserAccessesService, GetUserAccessesService>();

            services.AddScoped<IGetUserBranchAccessesService, GetUserBranchAccessesService>();
            services.AddScoped<IUserAccessBranchService, UserAccessBranchService>();


            services.AddScoped<ILoadJournalVoucherService, LoadJournalVoucherService>();
            services.AddScoped<IAddNewJournalVoucherSevice, AddNewJournalVoucherSevice>();

            services.AddScoped<IGetJournalVoucher, GetJournalVoucher>();


            services.AddScoped<ILoadPaymentVoucherService, LoadPaymentVoucherService>();

            services.AddScoped<ILoadReceiptVoucherService, LoadReceiptVoucherService>();

            //SuffixPrefix
            services.AddScoped<IAddNewSuffixPrefixService, AddNewSuffixPrefixService>();
            services.AddScoped<IGetSuffixPrefixInfoService, GetSuffixPrefixInfoService>();
            services.AddScoped<IGetSuffixPrefixService, GetSuffixPrefixService>();
            services.AddScoped<ILoadSuffixPrefixService, LoadSuffixPrefixService>();

            //Exchange Rate
            services.AddScoped<IGetExchangeRateService, GetExchangeRateService>();
            services.AddScoped<IGetExchangeRateInfoService, GetExchangeRateInfoService>();
            services.AddScoped<IAddNewExchangeRateService, AddNewExchangeRateService>();
            services.AddScoped<IDeleteExchangeRateService, DeleteExchangeRateService>();



            services.AddScoped<INavigateJornalVoucherService, NavigateJornalVoucherService>();

            //Benefeciary
            services.AddScoped<IAddNewBenefeciaryService, AddNewBenefeciaryService>();
            services.AddScoped<IDeleteBeneficiaryService, DeleteBeneficiaryService>();
            services.AddScoped<IGetBenefeciaryInfoService, GetBenefeciaryInfoService>();
            services.AddScoped<IGetBenefeciaryService, GetBenefeciaryService>();

            //Currency
            services.AddScoped<IAddNewCurrencyService, AddNewCurrencyService>();
            services.AddScoped<IDeleteCurrencyService, DeleteCurrencyService>();
            services.AddScoped<IGetCurrencyInfoService, GetCurrencyInfoService>();
            services.AddScoped<IGetCurrencyService, GetCurrencyService>();

            services.AddScoped<IGetCurrentDateService, GetCurrentDateService>();


            //Remittance
            services.AddScoped<ILoadRemittanceService, LoadRemittanceService>();
            services.AddScoped<IGetRemittanceForInsertService, GetRemittanceForInsertService>();


            services.AddScoped<IDeleteAccountLedgerService, DeleteAccountLedgerService>();


            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
            });
        }
    }

    public class CustomMetadataProvider : IMetadataDetailsProvider, IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {

            if (context.Key.MetadataKind == ModelMetadataKind.Property)
            {
                context.DisplayMetadata.ConvertEmptyStringToNull = false;
            }
        }
    }
}
