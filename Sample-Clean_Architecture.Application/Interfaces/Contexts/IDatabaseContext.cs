namespace Sample_Clean_Architecture.Application.Interfaces.Contexts
{
    public interface IDatabaseContext
    {

        int Sp_Company_Insert(RequestCompanyDto company);
    
        public int Sp_CompanyFinancialCycle_Insert(CompanyFinancialCycle_Dto dto);

        public int sp_CompanyBranch_Insert(CompanyBranch_Dto dto);

        public ResultCompanyUserDto Sp_CompanyUsers_Insert(CompanyUserDto dto);

        List<CompaniesList_Dto> Sp_Company_List(int Accounts_Id);

        public List<CompanyFinancialCycle_Dto> Sp_CompanyFinancialCycle_List(int Company_Id);

        public List<CompanyBranch_Dto> sp_CompanyBranch_List(int Company_Id);

        public CompanyBranch_Dto Sp_CompanyBranch_Get(int CompanyBranch_Id);

        public ResultUserloginDto Sp_Users_Login(string userName, string uassword, out byte errorType);
        public ResultUserForgotDto Sp_Users_Forget(string userName, out byte errorType);
        public int Sp_Users_UpdatePassword(int Users_Id, string Password);
        public List<Role> Role_GetAll();

        public InsertAccountDto Sp_Account_Insert(string email, string title, string password);

        public int Sp_Account_Activate(int UserId, int AccountId);

        public CompanyGetDto Sp_Company_Get(int Company_Id);

        public CompanyFinancialCycle_Dto Sp_CompanyFinancialCycle_Get(int FinancialCycle_Id);
        public List<CompanyUserDto> Sp_CompanyUsers_List(int Company_Id);

        public CompanyUserDto Sp_CompanyUser_Get(int companyUsers_Id);
        public ResultUserChangeDto Sp_CompanyUsers_Change(int CompanyUsers_Id);
        public int Sp_CompanyUsers_DeletePending(int CompanyUsers_Id, out bool Error);

        public List<ResultMenuDto> Sp_CompanyUsers_PolicyGet(int CompanyUsers_Id);

        public List<ResultUserBranchDto> sp_UsersAccess_Get(int Id, int Company_Id, byte kind);
        public AccountGroupListDto sp_AccountGroup_Get(int Company_Id, int CompanyUsers_Id);

        public int sp_AccountGroup_Insert(RequestAccountGroup accountGroup);
        public RequestAccountGroupDto sp_AccountGroup_GetById(int accountGroup_Id, int company_Id, int accountGroup_Parent);
        //ثبت دسترسی کاربر
        public int Sp_MenuOptionsUsers_Insert(int CompanyUsers_Id, string MenuOptions_Ids);

        public int sp_UsersAccess_Insert(int Id, string CompanyUsers_IdStr, byte kind);

        public int sp_AccountGroup_Delete(int accountGroup_Id, out bool Error);
        public int sp_AccountGroup_GetNature(int accountGroup_Id);
        public InFormAccess sp_AccountGroup_GetAccess(int CompanyUsers_Id);

        public int sp_UsersProfile_Insert(UserProfileDto userProfileDto);

        public AccountListDto sp_AccountLegder_Get(int Company_Id, int AccountGroup_Id, int companyUser_Id);
        public AccountLedgerDto sp_AccountLegder_GetById(int company_Id, int accountGroup_Id);
        public int sp_AccountLegder_Insert(AccountLedgerDto account);


        public CostCenterDto sp_CostCenter_GetById(int company_Id, int costCenter_Id);
        public int sp_CostCenter_Insert(int company_Id, CostCenterDto costCenter);
        public List<CostCenterListDto> sp_CostCenter_List(int company_Id);

        public ProjectDto sp_Projects_GetById(int company_Id, int project_id);
        public int sp_Project_Insert(int company_Id, ProjectDto project);
        public List<ProjectListDto> sp_Project_List(int company_id);
        public int sp_Projects_Delete(int projects_id, out bool Error);
        public int sp_CostCenter_Delete(int costcenter_id, out bool Error);

        public JournalVoucherLoadDto sp_JournalVoucher_Load(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);

        public ResultJournalVoucher sp_Voucher_Insert(RequestJournalVoucher journalVoucher);
        public JournalVoucherDto sp_Voucher_GetById(long VoucherMasters_Id);

        public OtherVoucherLoadDto sp_Voucher_PaymentLoad(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);
        public OtherVoucherLoadDto sp_Voucher_ReceiptLoad(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate);

        public int sp_SuffixPrefix_Insert(SuffixPrefix_Dto dto);
        public SuffixPrefix_Dto sp_SuffixPrefix_GetById(int SuffixPrefix_Id);
        public List<SuffixPrefix_Dto> sp_SuffixPrefix_Get(int Company_Id);
        public List<VoucherTypeDto> sp_SuffixPrefix_Load();
        public ExchangeRate_Dto sp_ExchangeRate_GetList(int Company_Id, DateTime? dateTime);
        public JournalVoucherDto sp_Voucher_Navigate(byte VoucherType_Id, long CurrentvoucherMasters_Id, byte Navigate_Status, out byte Error);
        public int sp_Company_GetDate(int Company_Id, out DateTime CurDate);

        public List<BenefeciaryList_Dto> sp_Beneficiary_List(int Company_Id);
        public int sp_Beneficiary_Insert(Benefeciary_Dto benefeciary);
        public int sp_Beneficiary_Delete(int beneficiary_id, out bool Error);
        public Benefeciary_Dto sp_Beneficiary_GetById(int beneficiary_id);
        public List<CurrencyList_Dto> sp_CurrencyCompany_List(int Company_Id);
        public Currency_Dto sp_CurrencyCompany_GetById(long currency_id);
        public int sp_CurrencyCompany_Insert(Currency_Dto currency);
        public int sp_CurrencyCompany_Delete(long currency_id, out bool Error);
        public ExchangeRateInfo_Dto sp_ExchangeRate_GetById(int Company_Id, long exchangerate_id, DateTime datetime);
        public int sp_ExchangeRate_Insert(ExchangeRateInfoById_Dto exchangerate);
        public int sp_ExchangeRate_Delete(long exchangerate_id, out bool Error);

        public RemittanceLoadDto sp_Remittance_LoadPage(int Company_Id, int VoucherTypeId, bool CurrentDate, DateTime VoucherDate);
        public List<RemittanceCurrenciesDto> sp_Remittance_GetForInsert(int Company_Id, long Currency_Id);
        public int sp_AccountLedger_Delete(int accountLedger_Id, out bool Error);
    }
}
