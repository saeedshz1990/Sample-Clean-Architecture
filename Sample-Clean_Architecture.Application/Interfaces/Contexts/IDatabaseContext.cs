using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountGroup;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountLedger;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccount;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccountGroup;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.AddNewBenefeciary;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Queries.GetBenefeciaries;
using Sample_Clean_Architecture.Application.Services.Common.Commands.UserProfile;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompany;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyUser;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanies;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyBranches;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyFinancialCycle;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyUsers;
using Sample_Clean_Architecture.Application.Services.CostCenter.Commands.AddNewCostCenter;
using Sample_Clean_Architecture.Application.Services.CostCenter.Queries.GetCostCenters;
using Sample_Clean_Architecture.Application.Services.Currencies.Queries.GetCurrencies;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Queries.GetExchangeRate;
using Sample_Clean_Architecture.Application.Services.Project.Commands.AddNewProject;
using Sample_Clean_Architecture.Application.Services.Project.Queries.GetProjects;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.LoadSuffixPrefix;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserChange;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserForgot;
using Sample_Clean_Architecture.Application.Services.Users.Commands.UserLogin;
using Sample_Clean_Architecture.Application.Services.Users.Queries.GetUserAccesses;
using Sample_Clean_Architecture.Application.Services.Users.Queries.GetUserBranchAccess;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.PaymentVoucher.Queries.LoadPaymentlVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.GetRemittanceForInsert;
using Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.LoadRemittance;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Domain.Entities.Users;

namespace Sample_Clean_Architecture.Application.Interfaces.Contexts
{
    public interface IDatabaseContext
    {

        /// <summary>
        /// ثبت شرکت
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        int Sp_Company_Insert(RequestCompanyDto company);
        /// <summary>
        /// ثبت دوره مالی شرکت
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int Sp_CompanyFinancialCycle_Insert(CompanyFinancialCycle_Dto dto);

        /// ثبت شعبه شرکت
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int sp_CompanyBranch_Insert(CompanyBranch_Dto dto);

        /// <summary>
        /// ثبت کاربر شرکت
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public ResultCompanyUserDto Sp_CompanyUsers_Insert(CompanyUserDto dto);

        /// <summary>
        /// دریافت لیست شرکت ها
        /// </summary>
        /// <returns></returns>
        List<CompaniesList_Dto> Sp_Company_List(int Accounts_Id);

        /// دریافت لیست دوره های مالی شرکت
        /// </summary>
        /// <returns></returns>
        public List<CompanyFinancialCycle_Dto> Sp_CompanyFinancialCycle_List(int Company_Id);

        /// دریافت لیست شعبه های شرکت
        public List<CompanyBranch_Dto> sp_CompanyBranch_List(int Company_Id);

        /// دریافت لیست شعبه شرکت
        public CompanyBranch_Dto Sp_CompanyBranch_Get(int CompanyBranch_Id);

        //ورود کاربر
        public ResultUserloginDto Sp_Users_Login(string userName, string uassword, out byte errorType);
        //فراموشی رمز عبور
        public ResultUserForgotDto Sp_Users_Forget(string userName, out byte errorType);
        //بروزرسانی کلمه عبور
        public int Sp_Users_UpdatePassword(int Users_Id, string Password);
        //دریافت نقش های تعریف شده
        public List<Role> Role_GetAll();

        //ثبت نام کاربر جدید - Creator
        public InsertAccountDto Sp_Account_Insert(string email, string title, string password);

        //فعال سازی کابر
        public int Sp_Account_Activate(int UserId, int AccountId);

        //دریافت اطلاعات شرکت
        public CompanyGetDto Sp_Company_Get(int Company_Id);

        //دریافت اطلاعات دوره مالی
        public CompanyFinancialCycle_Dto Sp_CompanyFinancialCycle_Get(int FinancialCycle_Id);
        // دریافت کاربران شرکت
        public List<CompanyUserDto> Sp_CompanyUsers_List(int Company_Id);

        //دریافت اطلاعات کاربر شرکت
        public CompanyUserDto Sp_CompanyUser_Get(int companyUsers_Id);
        // دریافت اطلاعات کاربر
        public ResultUserChangeDto Sp_CompanyUsers_Change(int CompanyUsers_Id);
        // حذف کاربر
        public int Sp_CompanyUsers_DeletePending(int CompanyUsers_Id, out bool Error);
        // دریافت لیست دسترسی ها

        public List<ResultMenuDto> Sp_CompanyUsers_PolicyGet(int CompanyUsers_Id);

        // دریافت لیست دسترسی کاربران شعبه

        public List<ResultUserBranchDto> sp_UsersAccess_Get(int Id, int Company_Id, byte kind);
        // دریافت لیست گروه ها
        public AccountGroupListDto sp_AccountGroup_Get(int Company_Id, int CompanyUsers_Id);

        // ثبت گروه اکانت
        public int sp_AccountGroup_Insert(RequestAccountGroup accountGroup);
        //دریافت اطلاعات گروه اکانت
        public RequestAccountGroupDto sp_AccountGroup_GetById(int accountGroup_Id, int company_Id, int accountGroup_Parent);
        //ثبت دسترسی کاربر
        public int Sp_MenuOptionsUsers_Insert(int CompanyUsers_Id, string MenuOptions_Ids);

        //ثبت دسترسی کاربر شعبه 
        public int sp_UsersAccess_Insert(int Id, string CompanyUsers_IdStr, byte kind);

        // حذف گروه اکانت
        public int sp_AccountGroup_Delete(int accountGroup_Id, out bool Error);
        //دریافت لیست nature
        public int sp_AccountGroup_GetNature(int accountGroup_Id);
        //دریافت دسترسی گروه اکانت
        public InFormAccess sp_AccountGroup_GetAccess(int CompanyUsers_Id);

        //ذخیره پروفایل کاربر
        public int sp_UsersProfile_Insert(UserProfileDto userProfileDto);

        //دریافت اکانت ها 
        // AcccountGroup_Id = 0 اکانت لجر
        // AcccountGroup_Id = 22 اکانت مشتری
        // AcccountGroup_Id = 26 اکانت تامین کننده
        public AccountListDto sp_AccountLegder_Get(int Company_Id, int AccountGroup_Id, int companyUser_Id);
        //دریافت اکانت با شناسه 
        public AccountLedgerDto sp_AccountLegder_GetById(int company_Id, int accountGroup_Id);
        //ذخیره مشخصات اکانت 
        public int sp_AccountLegder_Insert(AccountLedgerDto account);


        //گرفتن مشخصات کاست سنتر
        public CostCenterDto sp_CostCenter_GetById(int company_Id, int costCenter_Id);
        //ذخیره مشخصات کاست سنتر 
        public int sp_CostCenter_Insert(int company_Id, CostCenterDto costCenter);
        public List<CostCenterListDto> sp_CostCenter_List(int company_Id);

        #region Project
        public ProjectDto sp_Projects_GetById(int company_Id, int project_id);
        public int sp_Project_Insert(int company_Id, ProjectDto project);
        //لیست پروژه ها
        public List<ProjectListDto> sp_Project_List(int company_id);
        //حذف پروژه
        public int sp_Projects_Delete(int projects_id, out bool Error);
        //حذف کاست سنتر
        public int sp_CostCenter_Delete(int costcenter_id, out bool Error);




        #endregion
        //  لود جورنال ووچر
        public JournalVoucherLoadDto sp_JournalVoucher_Load(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);

        // ذخیره جورنال ووچر
        public ResultJournalVoucher sp_Voucher_Insert(RequestJournalVoucher journalVoucher);
        //دریافت ووچر
        public JournalVoucherDto sp_Voucher_GetById(long VoucherMasters_Id);

        //لود پیمنت ووچر
        public OtherVoucherLoadDto sp_Voucher_PaymentLoad(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);
        //لود رسیپت ووچر
        public OtherVoucherLoadDto sp_Voucher_ReceiptLoad(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);

        // ذخیره پیشوند
        public int sp_SuffixPrefix_Insert(SuffixPrefix_Dto dto);
        // دریافت یک پیشوند
        public SuffixPrefix_Dto sp_SuffixPrefix_GetById(int SuffixPrefix_Id);
        // دریافت پیشوند ها
        public List<SuffixPrefix_Dto> sp_SuffixPrefix_Get(int Company_Id);
        //لود پیشوندها
        public List<VoucherTypeDto> sp_SuffixPrefix_Load();
        //لود نرخهای وارد شده برای یک تاریخ
        public ExchangeRate_Dto sp_ExchangeRate_GetList(int Company_Id, DateTime? dateTime);
        //پیمایش جورنال ووچر
        public JournalVoucherDto sp_Voucher_Navigate(byte VoucherType_Id, long CurrentvoucherMasters_Id, byte Navigate_Status, out byte Error);
        // دریافت تاریخ جاری
        public int sp_Company_GetDate(int Company_Id, out DateTime CurDate);

        //لیست بنفیشیاری
        public List<BenefeciaryList_Dto> sp_Beneficiary_List(int Company_Id);
        //ذخیره بنفیشیاری
        public int sp_Beneficiary_Insert(Benefeciary_Dto benefeciary);
        //حذف بنفیشیاری
        public int sp_Beneficiary_Delete(int beneficiary_id, out bool Error);
        //لود یک بنفیشیاری
        public Benefeciary_Dto sp_Beneficiary_GetById(int beneficiary_id);
        //لیست کارنسی ها
        public List<CurrencyList_Dto> sp_CurrencyCompany_List(int Company_Id);
        //لود یک کارنسی
        public Currency_Dto sp_CurrencyCompany_GetById(long currency_id);
        //ایجاد وویرایش کارنسی
        public int sp_CurrencyCompany_Insert(Currency_Dto currency);
        //حذف یک کارنسی
        public int sp_CurrencyCompany_Delete(long currency_id, out bool Error);
        //اضافه / ویرایش یک نرخ
        public ExchangeRateInfo_Dto sp_ExchangeRate_GetById(int Company_Id, long exchangerate_id, DateTime datetime);
        //ذخیره کارنسی
        public int sp_ExchangeRate_Insert(ExchangeRateInfoById_Dto exchangerate);
        //حذف کارنسی
        public int sp_ExchangeRate_Delete(long exchangerate_id, out bool Error);

        //لود حواله
        public RemittanceLoadDto sp_Remittance_LoadPage(int Company_Id, int VoucherTypeId, bool CurrentDate, DateTime VoucherDate);
        //لود ریت بر اساس کارنسی
        public List<RemittanceCurrenciesDto> sp_Remittance_GetForInsert(int Company_Id, long Currency_Id);
        //حذف لجر
        public int sp_AccountLedger_Delete(int accountLedger_Id, out bool Error);
    }
}