using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountGroup;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountLedger;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccount;
using Sample_Clean_Architecture.Application.Services.Account.Queries.GetAccountGroup;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.AddNewBenefeciary;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Queries.GetBenefeciaries;
using Sample_Clean_Architecture.Application.Services.Common.Commands.UserProfile;
using Sample_Clean_Architecture.Application.Services.Common.Queries.GetMenuItem;
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
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;
using Sample_Clean_Architecture.Domain.Entities.Users;
using System.Data;
using System.Data.SqlClient;
using static Sample_Clean_Architecture.Common.Enums;

namespace Sample_Clean_Architecture.Persistence.Contexts
{
    public class DatabaseContext : IDatabaseContext
    {

        public string DumiSoftConnectionString;
        public string DumiERPConnectionString;
        public IConfiguration Configuration { get; }
        private readonly ILogger<DatabaseContext> _logger;
        public DatabaseContext(IConfiguration configuration, ILogger<DatabaseContext> logger)
        {
            Configuration = configuration;
            DumiSoftConnectionString = Configuration.GetConnectionString("DefaultConnection");
            DumiERPConnectionString = Configuration.GetConnectionString("DumiERPConnection");
            _logger = logger;
        }

        public CompanyGetDto Sp_Company_Get(int Company_Id)
        {

            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_Company_Get", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                conn.Open();
                adapter.Fill(ds);
                CompanyGetDto companyGetDto = new CompanyGetDto();
                List<CurrencyDto> currencyLst = new List<CurrencyDto>();
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                        currencyLst.Add(new CurrencyDto() { Currency_Id = dr["Currency_Id"].ToInt(), Currency_Name = dr["Currency_Name"].ToString(), Currency_Symbol = dr["Currency_Symbol"].ToString() });
                }
                companyGetDto.Currencies = currencyLst;

                List<DateFormatDto> dateFormatLst = new List<DateFormatDto>();
                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                        dateFormatLst.Add(new DateFormatDto() { DateFormats_Id = dr["DateFormats_Id"].ToByte(), DateFormats_Description = dr["DateFormats_Description"].ToString() });
                }
                companyGetDto.DateFormats = dateFormatLst;

                List<DefaultLedgerDto> defaultLedgerLst = new List<DefaultLedgerDto>();
                if (ds.Tables.Count > 3)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                        defaultLedgerLst.Add(new DefaultLedgerDto() { Id = dr["Id"].ToByte(), Title = dr["Title"].ToString() });
                }
                companyGetDto.DefaultLedgers = defaultLedgerLst;

                List<CountryDto> countries = new List<CountryDto>();
                if (ds.Tables.Count > 4)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                        countries.Add(new CountryDto() { Country_Id = dr["Country_Id"].ToByte(), Country_Name = dr["Country_Name"].ToString(), Currency_Name = dr["Currency_Name"].ToString(), Currency_Subunit = dr["Currency_Subunit"].ToString() });
                }
                companyGetDto.Countries = countries;

                DataTable dtCompany = ds.Tables[2];
                if (dtCompany.Rows.Count > 0)
                    companyGetDto.RequestCompany = new RequestCompanyDto()
                    {
                        Company_Id = Company_Id,
                        // Accounts_Id = Convertor.ToInt(dtCompany.Rows[0]["Accounts_Id"]),
                        Company_Address = dtCompany.Rows[0]["Company_Address"].ToString(),
                        Company_AliasName = dtCompany.Rows[0]["Company_AliasName"].ToString(),
                        Company_BusinessName = dtCompany.Rows[0]["Company_BusinessName"].ToString(),
                        Company_DateSeperator = dtCompany.Rows[0]["Company_DateSeperator"].ToByte(),
                        Company_Email = dtCompany.Rows[0]["Company_Email"].ToString(),
                        Company_Fax = dtCompany.Rows[0]["Company_Fax"].ToString(),
                        Company_Logo = (byte[])dtCompany.Rows[0]["Company_Logo"],
                        Company_Mobile = dtCompany.Rows[0]["Company_Mobile"].ToString(),
                        Company_PhoneNo = dtCompany.Rows[0]["Company_PhoneNo"].ToString(),
                        Company_PostalCode = dtCompany.Rows[0]["Company_PostalCode"].ToString(),
                        Company_Tax1 = dtCompany.Rows[0]["Company_Tax1"].ToString(),
                        Company_Tax2 = dtCompany.Rows[0]["Company_Tax2"].ToString(),
                        Company_Tax3 = dtCompany.Rows[0]["Company_Tax3"].ToString(),
                        Company_TransactionType = dtCompany.Rows[0]["Company_TransactionType"].ToByte(),
                        Company_WebAddress = dtCompany.Rows[0]["Company_WebAddress"].ToString(),
                        Country_Id = dtCompany.Rows[0]["Country_Id"].ToByte(),

                        Currency_Name = dtCompany.Rows[0]["Currency_Name"].ToString(),
                        // Currency_Symbol = dtCompany.Rows[0]["Currency_Symbol"].ToString(),

                        DateFormats_Id = dtCompany.Rows[0]["DateFormats_Id"].ToByte(),
                        FinancialCycle_FromDate = Convert.ToDateTime(dtCompany.Rows[0]["FinancialCycle_FromDate"]),
                        Company_LedgerInserted = dtCompany.Rows[0]["Company_LedgerInserted"].ToBool(),
                        Company_CurrencyAutoupdate = dtCompany.Rows[0]["Company_CurrencyAutoupdate"].ToBool(),
                        Currency_Subunit = dtCompany.Rows[0]["Currency_Subunit"].ToString()
                    };
                else
                {
                    companyGetDto.RequestCompany = new RequestCompanyDto() { DateFormats_Id = dateFormatLst[0].DateFormats_Id.ToByte() };
                }

                return companyGetDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }


        }
        public ResultUserChangeDto Sp_CompanyUsers_Change(int CompanyUsers_Id)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyUsers_Change", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", CompanyUsers_Id));
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                conn.Open();
                adapter.Fill(ds);
                ResultUserChangeDto resultUserChangeDto = new ResultUserChangeDto();


                if (ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        resultUserChangeDto.Menus.Add(new MenuItemDto() { Id = dr["MenuOptions_Id"].ToInt(), Title = dr["MenuOptions_Title"].ToString(), Url = dr["MenuOptions_Url"].ToString(), ParentId = dr["MenuOptions_ParentId"].ToInt() });
                    }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    resultUserChangeDto.Company_TransactionType = ds.Tables[1].Rows[0]["Company_TransactionType"].ToByte();
                    resultUserChangeDto.DateFormats_Id = ds.Tables[1].Rows[0]["DateFormats_Id"].ToByte();
                    resultUserChangeDto.Company_DateSeperator = ds.Tables[1].Rows[0]["Company_DateSeperator"].ToByte();
                    resultUserChangeDto.Company_Id = ds.Tables[1].Rows[0]["Company_Id"].ToInt();
                }
                else
                {
                    resultUserChangeDto.Company_TransactionType = 0;
                    resultUserChangeDto.DateFormats_Id = 0;
                    resultUserChangeDto.Company_DateSeperator = 0;
                }
                if (resultUserChangeDto.Company_TransactionType == 2 && ds.Tables[2].Rows.Count > 0)
                {
                    resultUserChangeDto.FinancialCycle_FromDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["FinancialCycle_FromDate"]);
                    resultUserChangeDto.FinancialCycle_ToDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["FinancialCycle_ToDate"]);
                }
                else
                {
                    resultUserChangeDto.FinancialCycle_FromDate = DateTime.MinValue;
                    resultUserChangeDto.FinancialCycle_ToDate = DateTime.MaxValue;
                }

                return resultUserChangeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }

        }
        public ResultUserloginDto Sp_Users_Login(string userName, string password, out byte errorType)
        {
            errorType = 0;
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_Users_Login", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserName", userName));
            command.Parameters.Add(new SqlParameter("@Password", password));
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                conn.Open();
                adapter.Fill(ds);
                ResultUserloginDto resultUserloginDto = new ResultUserloginDto();
                if (ds.Tables.Count > 0)
                    errorType = ds.Tables[0].Rows[0][errorType].ToByte();
                if (errorType != 0)
                    return resultUserloginDto;
                else
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        resultUserloginDto.Accounts_Id = ds.Tables[1].Rows[0]["Accounts_Id"].ToInt();
                        resultUserloginDto.Roles.Add("Creator");
                    }
                    else
                    {
                        resultUserloginDto.Accounts_Id = 0;
                        resultUserloginDto.Roles.Add("Operator");
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            resultUserloginDto.UserInBusiness.Add(new UserBusinessDto() { CompanyUsers_Id = dr["CompanyUsers_Id"].ToInt(), Company_BusinessName = dr["Company_BusinessName"].ToString() });
                        }

                    if (ds.Tables[3].Rows.Count > 0)
                        foreach (DataRow dr in ds.Tables[3].Rows)
                        {
                            resultUserloginDto.Menus.Add(new MenuItemDto() { Id = dr["MenuOptions_Id"].ToInt(), Title = dr["MenuOptions_Title"].ToString(), Url = dr["MenuOptions_Url"].ToString(), ParentId = dr["MenuOptions_ParentId"].ToInt() });
                        }

                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        resultUserloginDto.Company_TransactionType = ds.Tables[4].Rows[0]["Company_TransactionType"].ToByte();
                        resultUserloginDto.DateFormats_Id = ds.Tables[4].Rows[0]["DateFormats_Id"].ToByte();
                        resultUserloginDto.Company_DateSeperator = ds.Tables[4].Rows[0]["Company_DateSeperator"].ToByte();
                        resultUserloginDto.Company_Id = ds.Tables[4].Rows[0]["Company_Id"].ToInt();
                        resultUserloginDto.Users_Id = ds.Tables[4].Rows[0]["Users_Id"].ToInt();
                        resultUserloginDto.DateFormats_Description = ds.Tables[4].Rows[0]["DateFormats_Description"].ToString();
                    }
                    else
                    {
                        resultUserloginDto.Company_TransactionType = 0;
                        resultUserloginDto.DateFormats_Id = 0;
                        resultUserloginDto.Company_DateSeperator = 0;
                        resultUserloginDto.Company_Id = 0;
                        resultUserloginDto.Users_Id = 0;
                        resultUserloginDto.DateFormats_Description = "yyyy/mm/dd";

                    }
                    if (resultUserloginDto.Company_TransactionType == 2 && ds.Tables[5].Rows.Count > 0)
                    {
                        resultUserloginDto.FinancialCycle_FromDate = Convert.ToDateTime(ds.Tables[5].Rows[0]["FinancialCycle_FromDate"]);
                        resultUserloginDto.FinancialCycle_ToDate = Convert.ToDateTime(ds.Tables[5].Rows[0]["FinancialCycle_ToDate"]);
                    }
                    else
                    {
                        resultUserloginDto.FinancialCycle_FromDate = DateTime.MinValue;
                        resultUserloginDto.FinancialCycle_ToDate = DateTime.MaxValue;
                    }
                    if (ds.Tables[ds.Tables.Count - 1].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[ds.Tables.Count - 1].Rows[0];
                        resultUserloginDto.UserProfile = new UserProfileDto()
                        {
                            UsersProfile_isFullscreen = dr["UsersProfile_isFullscreen"].ToBool(),
                            UsersProfile_isLight = dr["UsersProfile_isLight"].ToBool(),
                            UsersProfile_TemplateKind = dr["UsersProfile_TemplateKind"].ToByte()
                        };
                    }
                    else
                        resultUserloginDto.UserProfile = new UserProfileDto() { UsersProfile_isFullscreen = false, UsersProfile_isLight = true, UsersProfile_TemplateKind = 1 };

                }
                return resultUserloginDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }

        }

        public ResultUserForgotDto Sp_Users_Forget(string userName, out byte errorType)
        {
            errorType = 0;
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_Users_Forget", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserName", userName));

            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                conn.Open();
                adapter.Fill(ds);
                ResultUserForgotDto resultUserForgotDto = new ResultUserForgotDto();
                if (ds.Tables.Count > 0)
                    errorType = ds.Tables[0].Rows[0][errorType].ToByte();
                if (errorType != 0)
                    return resultUserForgotDto;
                else
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        resultUserForgotDto.Accounts_Id = ds.Tables[1].Rows[0]["Accounts_Id"].ToInt();
                        resultUserForgotDto.Users_Id = ds.Tables[1].Rows[0]["Users_Id"].ToInt(); ;
                    }
                    else
                    {
                        resultUserForgotDto.Accounts_Id = 0;
                        resultUserForgotDto.Users_Id = 0;
                    }



                }
                return resultUserForgotDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }

        }

        public List<Role> Role_GetAll()
        {
            return new List<Role>() { new Role() {Id=1, Name="Creator" },
            new Role() {Id=2, Name="Operator" },
           };
        }
        public ResultCompanyUserDto Sp_CompanyUsers_Insert(CompanyUserDto dto)
        {

            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyUsers_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", dto.CompanyUsers_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", dto.Company_Id));
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Status", dto.CompanyUsers_Status));
            command.Parameters.Add(new SqlParameter("@Users_UserName", dto.Users_UserName));
            command.Parameters.Add(new SqlParameter("@Password", dto.Users_Password));
            command.Parameters.Add(new SqlParameter("@Users_Description", dto.Users_Description));
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                if (dt.Rows.Count > 0)
                    return new ResultCompanyUserDto() { StatusOpr = dt.Rows[0]["StatusOpr"].ToByte(), CompanyUsers_Id = dto.CompanyUsers_Id, Users_Id = dt.Rows[0]["Users_Id"].ToInt() };
                else
                    return new ResultCompanyUserDto();

            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new ResultCompanyUserDto() { StatusOpr = 5, CompanyUsers_Id = dto.CompanyUsers_Id, Users_Id = 0 };
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_CompanyBranch_Insert(CompanyBranch_Dto dto)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("sp_CompanyBranch_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@CompanyBranch_Id", dto.CompanyBranch_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", dto.Company_Id));
            command.Parameters.Add(new SqlParameter("@CompanyBranch_Title", dto.CompanyBranch_Title));

            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public int Sp_CompanyFinancialCycle_Insert(CompanyFinancialCycle_Dto dto)
        {

            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyFinancialCycle_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@FinancialCycle_Id", dto.FinancialCycle_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", dto.Company_Id));
            command.Parameters.Add(new SqlParameter("@FinancialCycle_FromDate", dto.FinancialCycle_FromDate));
            command.Parameters.Add(new SqlParameter("@FinancialCycle_isActive", dto.FinancialCycle_isActive));
            command.Parameters.Add(new SqlParameter("@FinancialCycle_Title", dto.FinancialCycle_Title));
            command.Parameters.Add(new SqlParameter("@FinancialCycle_ToDate", dto.FinancialCycle_ToDate));
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public CompanyUserDto Sp_CompanyUser_Get(int companyUsers_Id)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyUsers_Get", conn);
            command.Parameters.Add(new SqlParameter("@companyUsers_Id", companyUsers_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                if (dt.Rows.Count > 0)
                    return new CompanyUserDto() { CompanyUsers_Id = companyUsers_Id, Users_UserName = dt.Rows[0]["Users_UserName"].ToString(), Users_Description = dt.Rows[0]["Users_Description"].ToString(), CompanyUsers_Status = (CompanyUserStatus)dt.Rows[0]["CompanyUsers_Status"].ToByte(), Users_Status = (UserStatus)dt.Rows[0]["Users_Status"].ToByte() };
                return new CompanyUserDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<ResultMenuDto> Sp_CompanyUsers_PolicyGet(int companyUsers_Id)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyUsers_PolicyGet", conn);
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", companyUsers_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<ResultMenuDto> menuList = new List<ResultMenuDto>();

                foreach (DataRow dr in dt.Rows)
                {
                    menuList.Add(new ResultMenuDto()
                    {
                        MenuOptions_Id = dr["MenuOptions_Id"].ToInt(),
                        MenuOptions_Title = dr["MenuOptions_Title"].ToString(),
                        MenuOptions_ParentId = dr["MenuOptions_ParentId"].ToInt(),
                        CompanyUsers_MenuId = dr["CompanyUsers_MenuId"].ToInt(),
                        // MenuOptions_Icon = dr["MenuOptions_Icon"].ToString(),
                        // MenuOptions_Order = Convertor.ToInt(dr["MenuOptions_Order"]),
                        MenuOptions_IsMenu = dr["MenuOptions_IsMenu"].ToBool()
                    }); ;
                }

                return menuList;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new List<ResultMenuDto>();
            }
            finally
            {
                conn.Close();
            }
        }

        public List<ResultUserBranchDto> sp_UsersAccess_Get(int Id, int Company_Id, byte kind)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_UsersAccess_Get", conn);
            command.Parameters.Add(new SqlParameter("@Id", Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@Kind", kind));

            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<ResultUserBranchDto> userList = new List<ResultUserBranchDto>();

                foreach (DataRow dr in dt.Rows)
                {
                    userList.Add(new ResultUserBranchDto()
                    {
                        Id = dr["Id"].ToInt(),
                        Users_Description = dr["Users_Description"].ToString(),
                        CompanyUsers_Id = dr["CompanyUsers_Id"].ToInt(),
                        Users_UserName = dr["Users_UserName"].ToString()
                    }); ;
                }

                return userList;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new List<ResultUserBranchDto>();
            }
            finally
            {
                conn.Close();
            }
        }

        public CompanyFinancialCycle_Dto Sp_CompanyFinancialCycle_Get(int FinancialCycle_Id)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyFinancialCycle_Get", conn);
            command.Parameters.Add(new SqlParameter("@FinancialCycle_Id", FinancialCycle_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                if (dt.Rows.Count > 0)
                    return new CompanyFinancialCycle_Dto() { FinancialCycle_Title = dt.Rows[0]["FinancialCycle_Title"].ToString(), FinancialCycle_Id = FinancialCycle_Id, FinancialCycle_FromDate = Convert.ToDateTime(dt.Rows[0]["FinancialCycle_FromDate"]), FinancialCycle_ToDate = Convert.ToDateTime(dt.Rows[0]["FinancialCycle_ToDate"]), FinancialCycle_isActive = dt.Rows[0]["FinancialCycle_isActive"].ToBool() };
                return new CompanyFinancialCycle_Dto();
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<CompanyUserDto> Sp_CompanyUsers_List(int Company_Id)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyUsers_List", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<CompanyUserDto> companyUserLst = new List<CompanyUserDto>();
                foreach (DataRow dr in dt.Rows)
                {
                    companyUserLst.Add(new CompanyUserDto() { Company_Id = Company_Id, CompanyUsers_Id = dr["CompanyUsers_Id"].ToInt(), CompanyUsers_Status = (CompanyUserStatus)dr["CompanyUsers_Status"].ToByte(), Users_Description = dr["Users_Description"].ToString(), Users_Status = (UserStatus)dr["Users_Status"].ToByte(), Users_UserName = dr["Users_UserName"].ToString() });
                }
                return companyUserLst;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<CompanyBranch_Dto> sp_CompanyBranch_List(int Company_Id)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("sp_CompanyBranch_List", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<CompanyBranch_Dto> companyBrancheLst = new List<CompanyBranch_Dto>();
                foreach (DataRow dr in dt.Rows)
                {
                    companyBrancheLst.Add(new CompanyBranch_Dto() { Company_Id = Company_Id, CompanyBranch_Id = dr["CompanyBranch_Id"].ToInt(), CompanyBranch_Title = dr["CompanyBranch_Title"].ToString() });
                }
                return companyBrancheLst;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public CompanyBranch_Dto Sp_CompanyBranch_Get(int CompanyBranch_Id)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyBranch_Get", conn);
            command.Parameters.Add(new SqlParameter("@CompanyBranch_Id", CompanyBranch_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                if (dt.Rows.Count > 0)
                    return new CompanyBranch_Dto() { CompanyBranch_Title = dt.Rows[0]["CompanyBranch_Title"].ToString(), CompanyBranch_Id = CompanyBranch_Id };
                return new CompanyBranch_Dto();
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<CompanyFinancialCycle_Dto> Sp_CompanyFinancialCycle_List(int Company_Id)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyFinancialCycle_List", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<CompanyFinancialCycle_Dto> financialCycleLst = new List<CompanyFinancialCycle_Dto>();
                foreach (DataRow dr in dt.Rows)
                {
                    financialCycleLst.Add(new CompanyFinancialCycle_Dto() { Company_Id = Company_Id, FinancialCycle_Id = dr["FinancialCycle_Id"].ToInt(), FinancialCycle_Title = dr["FinancialCycle_Title"].ToString(), FinancialCycle_FromDate = Convert.ToDateTime(dr["FinancialCycle_FromDate"]), FinancialCycle_ToDate = Convert.ToDateTime(dr["FinancialCycle_ToDate"]), FinancialCycle_isActive = dr["FinancialCycle_isActive"].ToBool() });
                }
                return financialCycleLst;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<CompaniesList_Dto> Sp_Company_List(int Accounts_Id)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_Company_List", conn);
            command.Parameters.Add(new SqlParameter("@Accounts_Id", Accounts_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<CompaniesList_Dto> companyLst = new List<CompaniesList_Dto>();
                foreach (DataRow dr in dt.Rows)
                {
                    companyLst.Add(new CompaniesList_Dto() { Company_Id = dr["Company_Id"].ToInt(), Company_BusinessName = dr["Company_BusinessName"].ToString(), Company_AliasName = dr["Company_AliasName"].ToString(), Company_TransactionType = dr["Company_TransactionType"].ToByte() });
                }
                return companyLst;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return new List<CompaniesList_Dto>();
            }
            finally
            {
                conn.Close();
            }

        }
        public InsertAccountDto Sp_Account_Insert(string email, string title, string password)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_Account_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Email", email));
            command.Parameters.Add(new SqlParameter("@Title", title));
            command.Parameters.Add(new SqlParameter("@Password", password));

            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());

                return new InsertAccountDto() { Accounts_Id = dt.Rows[0]["AccountId"].ToInt(), Users_Id = dt.Rows[0]["UserId"].ToInt(), Error = dt.Rows[0]["Error"].ToByte() };
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return new InsertAccountDto();
            }
            finally
            {
                conn.Close();
            }
        }


        public int Sp_Users_UpdatePassword(int Users_Id, string Password)
        {

            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_Users_UpdatePassword", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Users_Id", Users_Id));
            command.Parameters.Add(new SqlParameter("@Password", Password));
            try
            {
                conn.Open();

                command.ExecuteNonQuery();
                return 1;

            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);


                return -1;

            }
            finally
            {
                conn.Close();
            }
        }
        public int sp_Company_GetDate(int Company_Id, out DateTime CurDate)
        {


            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Company_GetDate", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@CurDate", SqlDbType.DateTime)).Direction = ParameterDirection.Output;
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                CurDate = command.Parameters["@CurDate"].Value.ToDateTime();

                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                CurDate = "2079-01-01".ToDateTime();
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public int Sp_Account_Activate(int users_Id, int accounts_Id)
        {
            int Error = 0;
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_Account_Activate", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Users_Id", users_Id));
            command.Parameters.Add(new SqlParameter("@Accounts_Id", accounts_Id));
            command.Parameters.Add(new SqlParameter("@Activated", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                Error = Convertor.ToInt(command.Parameters["@Activated"].Value);

                return Error;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public int Sp_Company_Insert(RequestCompanyDto company)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_Company_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Accounts_Id", company.Accounts_Id));
            command.Parameters.Add(new SqlParameter("@Company_Address", company.Company_Address));
            command.Parameters.Add(new SqlParameter("@Company_AliasName", company.Company_AliasName));
            command.Parameters.Add(new SqlParameter("@Company_BusinessName", company.Company_BusinessName));
            command.Parameters.Add(new SqlParameter("@Company_DateSeperator", company.Company_DateSeperator));
            command.Parameters.Add(new SqlParameter("@Company_Email", company.Company_Email));
            command.Parameters.Add(new SqlParameter("@Company_Fax", company.Company_Fax));
            command.Parameters.Add(new SqlParameter("@Company_Id", company.Company_Id));
            command.Parameters.Add(new SqlParameter("@Company_Logo", company.Company_Logo));
            command.Parameters.Add(new SqlParameter("@Company_Mobile", company.Company_Mobile));
            command.Parameters.Add(new SqlParameter("@Company_PhoneNo", company.Company_PhoneNo));
            command.Parameters.Add(new SqlParameter("@Company_PostalCode", company.Company_PostalCode));
            command.Parameters.Add(new SqlParameter("@Company_Tax1", company.Company_Tax1));
            command.Parameters.Add(new SqlParameter("@Company_Tax2", company.Company_Tax2));
            command.Parameters.Add(new SqlParameter("@Company_Tax3", company.Company_Tax3));
            command.Parameters.Add(new SqlParameter("@Company_TransactionType", company.Company_TransactionType));
            command.Parameters.Add(new SqlParameter("@Company_WebAddress", company.Company_WebAddress));
            command.Parameters.Add(new SqlParameter("@Country_Id", company.Country_Id));

            command.Parameters.Add(new SqlParameter("@Country_Currency_Id", company.Country_Currency_Id));
            command.Parameters.Add(new SqlParameter("@Currency_Sunbunit", company.Currency_Subunit));


            command.Parameters.Add(new SqlParameter("@DateFormats_Id", company.DateFormats_Id));
            command.Parameters.Add(new SqlParameter("@FinancialCycle_FromDate", company.FinancialCycle_FromDate));

            command.Parameters.Add(new SqlParameter("@Company_LedgerInserted", company.DefaultLedger_Id));

            command.Parameters.Add(new SqlParameter("@Company_CurrencyAutoupdate", company.Company_CurrencyAutoupdate));
            //command.Parameters.Add(new SqlParameter("@Currency_Name", company.Currency_Name));

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public int Sp_MenuOptionsUsers_Insert(int CompanyUsers_Id, string MenuOptions_Ids)
        {

            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_MenuOptionsUsers_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", CompanyUsers_Id));
            command.Parameters.Add(new SqlParameter("@MenuOptions_Ids", MenuOptions_Ids));
            try
            {
                conn.Open();

                command.ExecuteNonQuery();
                return 1;

            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);


                return -1;

            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_UsersProfile_Insert(UserProfileDto userProfileDto)
        {
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("sp_UsersProfile_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Users_Id", userProfileDto.Users_Id));
            command.Parameters.Add(new SqlParameter("@UsersProfile_isFullscreen", userProfileDto.UsersProfile_isFullscreen));
            command.Parameters.Add(new SqlParameter("@UsersProfile_TemplateKind", userProfileDto.UsersProfile_TemplateKind));
            command.Parameters.Add(new SqlParameter("@UsersProfile_isLight", userProfileDto.UsersProfile_isLight));
            command.Parameters.Add(new SqlParameter("@InsertKind", userProfileDto.InsertKind));

            try
            {
                conn.Open();

                command.ExecuteNonQuery();
                return 1;

            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);


                return -1;

            }
            finally
            {
                conn.Close();
            }
        }
        public int sp_UsersAccess_Insert(int Id, string CompanyUsers_IdStr, byte kind)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_UsersAccess_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Id", Id));
            command.Parameters.Add(new SqlParameter("@CompanyUsers_IdStr", CompanyUsers_IdStr));
            command.Parameters.Add(new SqlParameter("@Kind", kind));

            try
            {
                conn.Open();

                command.ExecuteNonQuery();
                return 1;

            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);


                return -1;

            }
            finally
            {
                conn.Close();
            }
        }

        #region Dumi_ERP


        public int sp_AccountGroup_GetNature(int accountGroup_Id)
        {
            int nature_Id = 0;
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountGroup_GetNature", conn);
            command.Parameters.Add(new SqlParameter("@AccountGroup_Id", accountGroup_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());

                if (dt != null && dt.Rows.Count > 0)
                    nature_Id = dt.Rows[0]["Nature_Id"].ToInt();

                return nature_Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return -2;
            }
            finally
            {
                conn.Close();
            }

        }
        public RequestAccountGroupDto sp_AccountGroup_GetById(int accountGroup_Id, int company_Id, int accountGroup_Parent)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountGroup_GetById", conn);
            command.Parameters.Add(new SqlParameter("@AccountGroup_Id", accountGroup_Id));
            command.Parameters.Add(new SqlParameter("@Company_id", company_Id));
            command.Parameters.Add(new SqlParameter("@AccountGroup_Parent", accountGroup_Parent));



            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                conn.Open();
                adapter.Fill(ds);
                RequestAccountGroupDto requestAccountGroupDto = new RequestAccountGroupDto();
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                        requestAccountGroupDto.AccountGroups.Add(new AccountGroup()
                        {
                            AccountGroup_Id = dr["AccountGroup_Id"].ToInt(),
                            AccountGroup_Name = dr["AccountGroup_Name"].ToString()
                        });
                }
                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                        requestAccountGroupDto.Natures.Add(new Nature()
                        {
                            Nature_Id = dr["Nature_Id"].ToInt(),
                            Nature_Description = dr["Nature_Description"].ToString()
                        });
                }
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                    requestAccountGroupDto.RequestAccountGroup = new RequestAccountGroup() { AccountGroup_Id = accountGroup_Id, Company_Id = ds.Tables[2].Rows[0]["Company_Id"].ToInt(), AccountGroup_Name = ds.Tables[2].Rows[0]["AccountGroup_Name"].ToString(), AccountGroup_Narration = ds.Tables[2].Rows[0]["AccountGroup_Narration"].ToString(), AccountGroup_Parent = ds.Tables[2].Rows[0]["AccountGroup_Parent"].ToInt(), GrossProfit = ds.Tables[2].Rows[0]["GrossProfit"].ToBool(), Nature_Id = ds.Tables[2].Rows[0]["Nature_Id"].ToByte() };
                else
                    if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
                    // requestAccountGroupDto.RequestAccountGroupParent = new RequestAccountGroupParent() { AccountGroup_Parent = ds.Tables[4].Rows[0]["AccountGroup_Parent"].ToInt(), GrossProfit = ds.Tables[4].Rows[0]["AccountGroup_AffectGrossProfit"].ToBool(), Nature_Id = ds.Tables[4].Rows[0]["Nature_Id"].ToByte() };
                    requestAccountGroupDto.RequestAccountGroup = new RequestAccountGroup() { Company_Id = -1, AccountGroup_Parent = accountGroup_Parent/*ds.Tables[4].Rows[0]["AccountGroup_Parent"].ToInt()*/, GrossProfit = ds.Tables[4].Rows[0]["AccountGroup_AffectGrossProfit"].ToBool(), Nature_Id = ds.Tables[4].Rows[0]["Nature_Id"].ToByte() };
                else
                    requestAccountGroupDto.RequestAccountGroup = new RequestAccountGroup() { Company_Id = -1 };

                if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                    requestAccountGroupDto.HasChild = ds.Tables[3].Rows[0]["HasChild"].ToBool();


                return requestAccountGroupDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public InFormAccess sp_AccountGroup_GetAccess(int CompanyUsers_Id)
        {

            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountGroup_GetAccess", conn);
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", CompanyUsers_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                InFormAccess inFormAccess = new InFormAccess();
                if (dt.Rows.Count > 0)
                    inFormAccess = new InFormAccess() { Insert_Row = dt.Rows[0]["Insert_Row"].ToInt(), Edit_Row = dt.Rows[0]["Edit_Row"].ToInt(), Delete_Row = dt.Rows[0]["Delete_Row"].ToInt() };

                return inFormAccess;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }

        }
        public AccountGroupListDto sp_AccountGroup_Get(int company_Id, int CompanyUsers_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountGroup_Get", conn);
            command.Parameters.Add(new SqlParameter("@Company_Id", company_Id));
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", CompanyUsers_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                conn.Open();
                adapter.Fill(ds);
                AccountGroupListDto accountGroupListDto = new AccountGroupListDto();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        accountGroupListDto.AccountGroupList.Add(new AccountGroupList()
                        {
                            AccountGroup_Id = dr["AccountGroup_Id"].ToInt(),
                            AccountGroup_Name = dr["AccountGroup_Name"].ToString(),
                            AccountGroup_Parent = dr["AccountGroup_Parent"].ToInt(),
                            Company_Id = dr["Company_Id"].ToInt()
                        }); ;
                    }
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    accountGroupListDto.InFormAccess = new InFormAccess() { Insert_Row = ds.Tables[1].Rows[0]["Insert_Row"].ToInt(), Edit_Row = ds.Tables[1].Rows[0]["Edit_Row"].ToInt(), Delete_Row = ds.Tables[1].Rows[0]["Delete_Row"].ToInt() };


                return accountGroupListDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new AccountGroupListDto();
            }
            finally
            {
                conn.Close();
            }
        }
        public int sp_AccountGroup_Insert(RequestAccountGroup accountGroup)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountGroup_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@AccountGroup_Id", accountGroup.AccountGroup_Id));
            command.Parameters.Add(new SqlParameter("@AccountGroup_Name", accountGroup.AccountGroup_Name));
            command.Parameters.Add(new SqlParameter("@AccountGroup_Narration", accountGroup.AccountGroup_Narration));
            command.Parameters.Add(new SqlParameter("@AccountGroup_Parent", accountGroup.AccountGroup_Parent));
            command.Parameters.Add(new SqlParameter("@Company_Id", accountGroup.Company_Id));
            command.Parameters.Add(new SqlParameter("@GrossProfit", accountGroup.GrossProfit));
            command.Parameters.Add(new SqlParameter("@Nature_Id", accountGroup.Nature_Id));

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public int sp_AccountGroup_Delete(int accountGroup_Id, out bool Error)
        {
            Error = false;
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountGroup_Delete", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@AccountGroup_Id", accountGroup_Id));
            command.Parameters.Add(new SqlParameter("@Deleted", SqlDbType.Bit)).Direction = ParameterDirection.Output;


            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                Error = Convertor.ToBool(command.Parameters["@Deleted"].Value);
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public int sp_AccountLedger_Delete(int accountLedger_Id, out bool Error)
        {
            Error = false;
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountLedger_Delete", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Ledger_Id", accountLedger_Id));
            command.Parameters.Add(new SqlParameter("@Deleted", SqlDbType.Bit)).Direction = ParameterDirection.Output;


            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                Error = Convertor.ToBool(command.Parameters["@Deleted"].Value);
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public int Sp_CompanyUsers_DeletePending(int CompanyUsers_Id, out bool Error)
        {
            Error = false;
            SqlConnection conn = new SqlConnection(DumiSoftConnectionString);
            SqlCommand command = new SqlCommand("Sp_CompanyUsers_DeletePending", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", CompanyUsers_Id));
            // command.Parameters.Add(new SqlParameter("@Deleted", SqlDbType.Bit)).Direction = ParameterDirection.Output;


            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                // Error = Convertor.ToBool(command.Parameters["@Deleted"].Value);
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        public AccountListDto sp_AccountLegder_Get(int Company_Id, int AccountGroup_Id, int CompanyUser_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountLedger_Get", conn);
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@AccountGroup_Id", AccountGroup_Id));
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", CompanyUser_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                AccountListDto accountListDto = new AccountListDto();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        accountListDto.AccountList.Add(new AccountList()
                        {
                            Account_Node_Id = dr["Node_Id"].ToInt(),
                            Account_Node_Name = dr["Node_Name"].ToString(),
                            Account_AccountGroup_Parent = dr["AccountGroup_Parent"].ToInt(),
                            Account_Is_Group = dr["IsGroup"].ToInt()
                        });
                    }
                }
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    accountListDto.InFormAccess = new InFormAccess()
                    {
                        Insert_Row = ds.Tables[1].Rows[0]["Insert_Row"].ToInt(),
                        Edit_Row = ds.Tables[1].Rows[0]["Edit_Row"].ToInt(),
                        Delete_Row = ds.Tables[1].Rows[0]["Delete_Row"].ToInt()
                    };
                }

                return accountListDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new AccountListDto();
            }
            finally
            {
                conn.Close();
            }

        }

        public AccountLedgerDto sp_AccountLegder_GetById(int company_Id, int accountLedger_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountLedger_GetById", conn);
            command.Parameters.Add(new SqlParameter("@Ledger_Id", accountLedger_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", company_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                AccountLedgerDto accountLedgerDto = new AccountLedgerDto();

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        accountLedgerDto.Ledger_Id = accountLedger_Id;
                        accountLedgerDto.Ledger_Name = dr["Ledger_Name"].ToString();
                        accountLedgerDto.AccountGroup_Id = dr["AccountGroup_Id"].ToInt();
                        accountLedgerDto.Ledger_Code = dr["Ledger_Code"].ToInt();
                        accountLedgerDto.Currency_Id = dr["Currency_Id"].ToByte();
                        accountLedgerDto.Ledger_BillByBill = dr["Ledger_BillByBill"].ToBool();
                        accountLedgerDto.Ledger_Status = dr["Ledger_Status"].ToInt();
                        accountLedgerDto.LedgerBank_BankName = dr["LedgerBank_BankName"].ToString();
                        accountLedgerDto.LedgerBank_BranchName = dr["LedgerBank_BranchName"].ToString();
                        accountLedgerDto.LedgerBank_BranchCode = dr["LedgerBank_BranchCode"].ToString();
                        accountLedgerDto.LedgerBank_AccountNumber = dr["LedgerBank_AccountNumber"].ToString();
                        accountLedgerDto.LedgerBank_AccountName = dr["LedgerBank_AccountName"].ToString();
                        accountLedgerDto.LedgerBank_IBAN = dr["LedgerBank_IBAN"].ToString();
                        accountLedgerDto.LedgerBank_Swift = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerBank_HeaderNote = dr["LedgerBank_HeaderNote"].ToString();
                        accountLedgerDto.LedgerBank_FooterNote = dr["LedgerBank_FooterNote"].ToString();
                        accountLedgerDto.LedgerDetails_CreditLimit = (decimal)dr["LedgerDetails_CreditLimit"].ToFloat();
                        accountLedgerDto.LedgerDetails_CreditPeriod = dr["LedgerDetails_CreditPeriod"].ToInt();
                        accountLedgerDto.LedgerDetails_MailingName = dr["LedgerDetails_MailingName"].ToString();
                        accountLedgerDto.LedgerDetails_Branch = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerDetails_Email = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerDetails_Address = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerDetails_ContactPerson = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerDetails_Mobile1 = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerDetails_Mobile2 = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerDetails_Phone = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerDetails_Fax = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerDetails_Narration = dr["LedgerBank_Swift"].ToString();
                        accountLedgerDto.LedgerBank_IBAN = dr["LedgerBank_IBAN"].ToString();
                        accountLedgerDto.LedgerDetails_BankBranchName = dr["LedgerDetails_BankBranchName"].ToString();
                        accountLedgerDto.LedgerDetails_BankBranchCode = dr["LedgerDetails_BankBranchCode"].ToString();
                        accountLedgerDto.LedgerDetails_BankSwiftCode = dr["LedgerDetails_BankSwiftCode"].ToString();
                        accountLedgerDto.LedgerDetails_BankAccountNumber = dr["LedgerDetails_BankAccountNumber"].ToString();
                        accountLedgerDto.LedgerDetails_BankNameOnCheque = dr["LedgerDetails_BankNameOnCheque"].ToString();
                        accountLedgerDto.LedgerDetails_ShipTo = dr["LedgerDetails_ShipTo"].ToString();
                        accountLedgerDto.TermsAndCondition_Id = dr["TermsAndCondition_Id"].ToInt();
                        accountLedgerDto.LedgerDetails_CST = dr["LedgerDetails_CST"].ToString();
                        accountLedgerDto.LedgerDetails_TIN = dr["LedgerDetails_TIN"].ToString();
                        accountLedgerDto.LedgerDetails_VAT = dr["LedgerDetails_VAT"].ToString();
                        accountLedgerDto.LedgerDetails_PAN = dr["LedgerDetails_PAN"].ToString();
                    }

                }
                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                        accountLedgerDto.AcountGroupList.Add(new AccountGroup()
                        {
                            AccountGroup_Id = dr["AccountGroup_Id"].ToInt(),
                            AccountGroup_Name = dr["AccountGroup_Namen"].ToString()
                        });
                }

                if (ds.Tables.Count > 2)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                        accountLedgerDto.TermsAndConditionList.Add(new TermsAndCondition()
                        {
                            TermsAndCondition_Id = dr["TermsAndCondition_Id"].ToInt(),
                            TermsAndCondition_Name = dr["TermsAndCondition_Name"].ToString()
                        });
                }
                if (ds.Tables.Count > 3)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                        accountLedgerDto.CurrencyCompanyList.Add(new CurrencyCompany()
                        {
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            Currency_Name = dr["Currency_Name"].ToString()
                        });
                }

                return accountLedgerDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_AccountLegder_Insert(AccountLedgerDto account)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_AccountLedger_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Ledger_Id", account.Ledger_Id));
            command.Parameters.Add(new SqlParameter("@Ledger_Name", account.Ledger_Name));
            command.Parameters.Add(new SqlParameter("@AccountGroup_Id", account.AccountGroup_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", account.Company_Id));
            command.Parameters.Add(new SqlParameter("@Ledger_Code", account.Ledger_Code));
            command.Parameters.Add(new SqlParameter("@Currency_Id", account.Currency_Id));
            command.Parameters.Add(new SqlParameter("@Ledger_BillByBill", account.Ledger_BillByBill));
            command.Parameters.Add(new SqlParameter("@Ledger_Status", account.Ledger_Status));
            command.Parameters.Add(new SqlParameter("@LedgerBank_BankName", account.LedgerBank_BankName));
            command.Parameters.Add(new SqlParameter("@LedgerBank_BranchName", account.LedgerBank_BranchName));
            command.Parameters.Add(new SqlParameter("@LedgerBank_BranchCode", account.LedgerBank_BranchCode));
            command.Parameters.Add(new SqlParameter("@LedgerBank_AccountNumber", account.LedgerBank_AccountNumber));
            command.Parameters.Add(new SqlParameter("@LedgerBank_AccountName", account.LedgerBank_AccountName));
            command.Parameters.Add(new SqlParameter("@LedgerBank_IBAN", account.LedgerBank_IBAN));
            command.Parameters.Add(new SqlParameter("@LedgerBank_HeaderNote", account.LedgerBank_HeaderNote));
            command.Parameters.Add(new SqlParameter("@LedgerBank_FooterNote", account.LedgerBank_FooterNote));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_CreditLimit", account.LedgerDetails_CreditLimit));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_CreditPeriod", account.LedgerDetails_CreditPeriod));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_MailingName", account.LedgerDetails_MailingName));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_Branch", account.LedgerDetails_Branch));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_Email", account.LedgerDetails_Email));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_Address", account.LedgerDetails_Address));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_ContactPerson", account.LedgerDetails_ContactPerson));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_Mobile1", account.LedgerDetails_Mobile1));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_Mobile2", account.@LedgerDetails_Mobile2));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_Phone", account.LedgerDetails_Phone));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_Fax", account.LedgerDetails_Fax));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_Narration", account.LedgerDetails_Narration));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_BankIBAN", account.LedgerDetails_BankIBAN));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_BankBranchName", account.LedgerDetails_BankBranchName));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_BankBranchCode", account.LedgerDetails_BankBranchCode));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_BankSwiftCode", account.LedgerDetails_BankSwiftCode));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_BankAccountNumber", account.LedgerDetails_BankAccountNumber));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_BankNameOnCheque", account.LedgerDetails_BankNameOnCheque));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_ShipTo", account.LedgerDetails_ShipTo));
            command.Parameters.Add(new SqlParameter("@TermsAndCondition_Id", account.TermsAndCondition_Id));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_CST", account.LedgerDetails_CST));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_TIN", account.LedgerDetails_TIN));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_VAT", account.LedgerDetails_VAT));
            command.Parameters.Add(new SqlParameter("@LedgerDetails_PAN", account.LedgerDetails_PAN));

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        #region Voucher
        public OtherVoucherLoadDto sp_Voucher_PaymentLoad(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Voucher_PaymentLoad", conn);
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@Users_Id", Users_Id));
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", CompanyUsers_Id));
            command.Parameters.Add(new SqlParameter("@CurrentDate", CurrentDate));
            command.Parameters.Add(new SqlParameter("@VoucherDate", VoucherDate));

            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                OtherVoucherLoadDto paymentVoucherLoadDto = new OtherVoucherLoadDto();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    paymentVoucherLoadDto.VoucherNumber = ds.Tables[0].Rows[0]["VoucherNumber"].ToString();
                }
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    paymentVoucherLoadDto.VoucherDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["VoucherDate"]);
                }
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    paymentVoucherLoadDto.ProjectActive = ds.Tables[2].Rows[0]["ProjectActive"].ToBool();
                    paymentVoucherLoadDto.CostCenterActive = ds.Tables[2].Rows[0]["CostCenterActive"].ToBool();
                }

                if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        paymentVoucherLoadDto.CurrencyCompanyList.Add(new CurrencyCompany()
                        {
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            Currency_Name = dr["Currency_Name"].ToString(),
                            Rate = dr["Rate"].ToFloat()
                        });
                    }
                }
                if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        paymentVoucherLoadDto.CompanyBranchList.Add(new CompanyBranch()
                        {
                            CompanyBranch_Id = dr["CompanyBranch_Id"].ToInt(),
                            CompanyBranch_Title = dr["CompanyBranch_Title"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 5 && ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[5].Rows)
                    {
                        paymentVoucherLoadDto.ProjectList.Add(new Project()
                        {
                            Projects_Id = dr["Projects_Id"].ToInt(),
                            Projects_Name = dr["Projects_Name"].ToString()

                        });
                    }
                }
                if (ds.Tables.Count > 6 && ds.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[6].Rows)
                    {
                        paymentVoucherLoadDto.PaymentReceiptAccountList.Add(new PaymentReceiptAccount()
                        {
                            Ledger_Id = dr["Ledger_Id"].ToInt(),
                            Ledger_Name = dr["Ledger_Name"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 7 && ds.Tables[7].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[7].Rows)
                    {
                        paymentVoucherLoadDto.AccountLedgerList.Add(new AccountLedger()
                        {
                            Ledger_Id = dr["Ledger_Id"].ToInt(),
                            Ledger_Name = dr["Ledger_Name"].ToString(),
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            BillbyBill = dr["Currency_Id"].ToBool()
                        });
                    }
                }
                if (ds.Tables.Count > 8 && ds.Tables[8].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[8].Rows)
                    {
                        paymentVoucherLoadDto.CostCenterList.Add(new CostCenter()
                        {
                            CostCenter_Id = dr["CostCenter_Id"].ToInt(),
                            CostCenter_Name = dr["CostCenter_Name"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 9 && ds.Tables[9].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[9].Rows)
                    {
                        paymentVoucherLoadDto.VoucherTypeList.Add(new VoucherType()
                        {
                            VoucherLabel_Id = dr["VoucherLabel_Id"].ToInt(),
                            VoucherLabel_Title = dr["VoucherLabel_Title"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 10 && ds.Tables[10].Rows.Count > 0)
                {
                    paymentVoucherLoadDto.InFormAccess = new InFormAccess()
                    {
                        Insert_Row = ds.Tables[10].Rows[0]["Insert_Row"].ToInt(),
                        Edit_Row = ds.Tables[10].Rows[0]["Edit_Row"].ToInt(),
                        Delete_Row = ds.Tables[10].Rows[0]["Delete_Row"].ToInt()
                    };
                }

                return paymentVoucherLoadDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new OtherVoucherLoadDto();
            }
            finally
            {
                conn.Close();
            }
        }

        public OtherVoucherLoadDto sp_Voucher_ReceiptLoad(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Voucher_ReceiptLoad", conn);
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@Users_Id", Users_Id));
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", CompanyUsers_Id));
            command.Parameters.Add(new SqlParameter("@CurrentDate", CurrentDate));
            command.Parameters.Add(new SqlParameter("@VoucherDate", VoucherDate));

            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                OtherVoucherLoadDto receiptVoucherLoadDto = new OtherVoucherLoadDto();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    receiptVoucherLoadDto.VoucherNumber = ds.Tables[0].Rows[0]["VoucherNumber"].ToString();
                }
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    receiptVoucherLoadDto.VoucherDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["VoucherDate"]);
                }
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    receiptVoucherLoadDto.ProjectActive = ds.Tables[2].Rows[0]["ProjectActive"].ToBool();
                    receiptVoucherLoadDto.CostCenterActive = ds.Tables[2].Rows[0]["CostCenterActive"].ToBool();
                }

                if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        receiptVoucherLoadDto.CurrencyCompanyList.Add(new CurrencyCompany()
                        {
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            Currency_Name = dr["Currency_Name"].ToString(),
                            Rate = dr["Rate"].ToFloat()
                        });
                    }
                }
                if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        receiptVoucherLoadDto.CompanyBranchList.Add(new CompanyBranch()
                        {
                            CompanyBranch_Id = dr["CompanyBranch_Id"].ToInt(),
                            CompanyBranch_Title = dr["CompanyBranch_Title"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 5 && ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[5].Rows)
                    {
                        receiptVoucherLoadDto.ProjectList.Add(new Project()
                        {
                            Projects_Id = dr["Projects_Id"].ToInt(),
                            Projects_Name = dr["Projects_Name"].ToString()

                        });
                    }
                }
                if (ds.Tables.Count > 6 && ds.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[6].Rows)
                    {
                        receiptVoucherLoadDto.PaymentReceiptAccountList.Add(new PaymentReceiptAccount()
                        {
                            Ledger_Id = dr["Ledger_Id"].ToInt(),
                            Ledger_Name = dr["Ledger_Name"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 7 && ds.Tables[7].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[7].Rows)
                    {
                        receiptVoucherLoadDto.AccountLedgerList.Add(new AccountLedger()
                        {
                            Ledger_Id = dr["Ledger_Id"].ToInt(),
                            Ledger_Name = dr["Ledger_Name"].ToString(),
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            BillbyBill = dr["Currency_Id"].ToBool()
                        });
                    }
                }
                if (ds.Tables.Count > 8 && ds.Tables[8].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[8].Rows)
                    {
                        receiptVoucherLoadDto.CostCenterList.Add(new CostCenter()
                        {
                            CostCenter_Id = dr["CostCenter_Id"].ToInt(),
                            CostCenter_Name = dr["CostCenter_Name"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 9 && ds.Tables[9].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[9].Rows)
                    {
                        receiptVoucherLoadDto.VoucherTypeList.Add(new VoucherType()
                        {
                            VoucherLabel_Id = dr["VoucherLabel_Id"].ToInt(),
                            VoucherLabel_Title = dr["VoucherLabel_Title"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 10 && ds.Tables[10].Rows.Count > 0)
                {
                    receiptVoucherLoadDto.InFormAccess = new InFormAccess()
                    {
                        Insert_Row = ds.Tables[10].Rows[0]["Insert_Row"].ToInt(),
                        Edit_Row = ds.Tables[10].Rows[0]["Edit_Row"].ToInt(),
                        Delete_Row = ds.Tables[10].Rows[0]["Delete_Row"].ToInt()
                    };
                }

                return receiptVoucherLoadDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new OtherVoucherLoadDto();
            }
            finally
            {
                conn.Close();
            }
        }

        public JournalVoucherLoadDto sp_JournalVoucher_Load(int Company_Id, int Users_Id, int CompanyUsers_Id, bool CurrentDate, DateTime VoucherDate)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_JournalVoucher_Load", conn);
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@Users_Id", Users_Id));
            command.Parameters.Add(new SqlParameter("@CompanyUsers_Id", CompanyUsers_Id));
            command.Parameters.Add(new SqlParameter("@CurrentDate", CurrentDate));
            command.Parameters.Add(new SqlParameter("@VoucherDate", VoucherDate));

            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                JournalVoucherLoadDto journalVoucherLoadDto = new JournalVoucherLoadDto();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    journalVoucherLoadDto.VoucherNumber = ds.Tables[0].Rows[0]["VoucherNumber"].ToString();
                }
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    journalVoucherLoadDto.VoucherDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["VoucherDate"]);
                }
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    journalVoucherLoadDto.ProjectActive = ds.Tables[2].Rows[0]["ProjectActive"].ToBool();
                    journalVoucherLoadDto.CostCenterActive = ds.Tables[2].Rows[0]["CostCenterActive"].ToBool();
                }

                if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        journalVoucherLoadDto.CurrencyCompanyList.Add(new CurrencyCompany()
                        {
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            Currency_Name = dr["Currency_Name"].ToString(),
                            Rate = dr["Rate"].ToFloat()
                        });
                    }
                }
                if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        journalVoucherLoadDto.CompanyBranchList.Add(new CompanyBranch()
                        {
                            CompanyBranch_Id = dr["CompanyBranch_Id"].ToInt(),
                            CompanyBranch_Title = dr["CompanyBranch_Title"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 5 && ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[5].Rows)
                    {
                        journalVoucherLoadDto.ProjectList.Add(new Project()
                        {
                            Projects_Id = dr["Projects_Id"].ToInt(),
                            Projects_Name = dr["Projects_Name"].ToString()

                        });
                    }
                }
                if (ds.Tables.Count > 6 && ds.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[6].Rows)
                    {
                        journalVoucherLoadDto.AccountLedgerList.Add(new AccountLedger()
                        {
                            Ledger_Id = dr["Ledger_Id"].ToInt(),
                            Ledger_Name = dr["Ledger_Name"].ToString(),
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            BillbyBill = dr["Currency_Id"].ToBool()
                        });
                    }
                }
                if (ds.Tables.Count > 7 && ds.Tables[7].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[7].Rows)
                    {
                        journalVoucherLoadDto.CostCenterList.Add(new CostCenter()
                        {
                            CostCenter_Id = dr["CostCenter_Id"].ToInt(),
                            CostCenter_Name = dr["CostCenter_Name"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 8 && ds.Tables[8].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[8].Rows)
                    {
                        journalVoucherLoadDto.VoucherTypeList.Add(new VoucherType()
                        {
                            VoucherLabel_Id = dr["VoucherLabel_Id"].ToInt(),
                            VoucherLabel_Title = dr["VoucherLabel_Title"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 9 && ds.Tables[9].Rows.Count > 0)
                {
                    journalVoucherLoadDto.InFormAccess = new InFormAccess()
                    {
                        Insert_Row = ds.Tables[9].Rows[0]["Insert_Row"].ToInt(),
                        Edit_Row = ds.Tables[9].Rows[0]["Edit_Row"].ToInt(),
                        Delete_Row = ds.Tables[9].Rows[0]["Delete_Row"].ToInt()
                    };
                }

                return journalVoucherLoadDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new JournalVoucherLoadDto();
            }
            finally
            {
                conn.Close();
            }
        }


        public RemittanceLoadDto sp_Remittance_LoadPage(int Company_Id, int VoucherTypeId, bool CurrentDate, DateTime VoucherDate)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Remittance_LoadPage", conn);
            command.Parameters.Add(new SqlParameter("@voucherTypeId", VoucherTypeId));
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@CurrentDate", CurrentDate));
            command.Parameters.Add(new SqlParameter("@VoucherDate", VoucherDate));

            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                RemittanceLoadDto remittanceLoadDto = new RemittanceLoadDto();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    remittanceLoadDto.VoucherNumber = ds.Tables[0].Rows[0]["VoucherNumber"].ToString();
                }
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    remittanceLoadDto.VoucherDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["VoucherDate"]);
                }


                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        remittanceLoadDto.CurrencyCompanyList.Add(new CurrencyCompany()
                        {
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            Currency_Name = dr["Currency_Name"].ToString(),
                            Rate = dr["Rate"].ToFloat(),
                            ExchangeRate_Id = dr["ExchangeRate_Id"].ToLong()
                        });
                    }
                }

                if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        remittanceLoadDto.CustomerAccountList.Add(new PaymentReceiptAccount()
                        {
                            Ledger_Id = dr["Ledger_Id"].ToInt(),
                            Ledger_Name = dr["Ledger_Name"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        remittanceLoadDto.AccountLedgerList.Add(new AccountLedger()
                        {
                            Ledger_Id = dr["Ledger_Id"].ToInt(),
                            Ledger_Name = dr["Ledger_Name"].ToString(),
                            Currency_Id = dr["Currency_Id"].ToInt()
                        });
                    }
                }

                if (ds.Tables.Count > 5 && ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[5].Rows)
                    {
                        remittanceLoadDto.VoucherTypeList.Add(new VoucherType()
                        {
                            VoucherLabel_Id = dr["VoucherLabel_Id"].ToInt(),
                            VoucherLabel_Title = dr["VoucherLabel_Title"].ToString()
                        });
                    }
                }
                if (ds.Tables.Count > 6 && ds.Tables[6].Rows.Count > 0)
                {
                    remittanceLoadDto.RemittanceInfo = new RemittanceInfo()
                    {
                        Ledger_Name = ds.Tables[6].Rows[0]["Ledger_Name"].ToString(),
                        Remittance_AmountDecimalNo = ds.Tables[6].Rows[0]["Remittance_AmountDecimalNo"].ToByte(),
                        Remittance_BalanceDifference = ds.Tables[6].Rows[0]["Remittance_BalanceDifference"].ToDecimal(),
                        Remittance_BalanceDifferenceLedger = ds.Tables[6].Rows[0]["Remittance_BalanceDifferenceLedger"].ToLong(),
                        Remittance_RateDecimalNo = ds.Tables[6].Rows[0]["Remittance_RateDecimalNo"].ToByte()


                    };
                }
                if (ds.Tables.Count > 7 && ds.Tables[7].Rows.Count > 0)
                    remittanceLoadDto.DefaultCurrency_Id = ds.Tables[7].Rows[0]["DefaultCurrency_Id"].ToInt();

                if (ds.Tables.Count > 8 && ds.Tables[8].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[8].Rows)
                    {
                        remittanceLoadDto.RemittanceAllCurrencies.Add(new RemittanceAllCurrencies()
                        {
                            CurrencyName = dr["CurrencyName"].ToString(),
                            RemmitenceBatch_Rate = dr["RemmitenceBatch_Rate"].ToDecimal(),
                            RemmitenceBatch_Remaining = dr["RemmitenceBatch_Remaining"].ToDecimal(),
                            RemmitenceBatch_Tot = dr["RemmitenceBatch_Tot"].ToDecimal(),
                            RemmitenceBatch_Id = dr["RemmitenceBatch_Id"].ToInt()
                        });
                    }
                }
                return remittanceLoadDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new RemittanceLoadDto();
            }
            finally
            {
                conn.Close();
            }
        }

        public JournalVoucherDto sp_Voucher_GetById(long VoucherMasters_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Voucher_GetById", conn);
            command.Parameters.Add(new SqlParameter("@VoucherMasters_Id", VoucherMasters_Id));

            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                JournalVoucherDto journalVoucherDto = new JournalVoucherDto();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    journalVoucherDto.JournalVoucherMasterDto.Id = VoucherMasters_Id;
                    journalVoucherDto.JournalVoucherMasterDto.VoucherNo = ds.Tables[0].Rows[0]["voucherMasters_VoucherNo"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.InvoiceNo = ds.Tables[0].Rows[0]["voucherMasters_InvoiceNo"].ToString();

                    journalVoucherDto.JournalVoucherMasterDto.VoucherDate = ds.Tables[0].Rows[0]["voucherMasters_Date"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.Notes = ds.Tables[0].Rows[0]["voucherMasters_Narration"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.PublicNotes = ds.Tables[0].Rows[0]["voucherMasters_PublicNotes"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.RefNo = ds.Tables[0].Rows[0]["voucherMasters_RefNumber"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.RefNo2 = ds.Tables[0].Rows[0]["voucherMasters_RefNumber2"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.Branch_Id = ds.Tables[0].Rows[0]["CompanyBranch_Id"].ToInt();
                    journalVoucherDto.JournalVoucherMasterDto.FinancialCycle_Id = ds.Tables[0].Rows[0]["FinancialCycle_Id"].ToInt();
                }
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        journalVoucherDto.JournalVoucherDetailsDto.Add(new JournalVoucherDetailDto()
                        {
                            Id = dr["VoucherDetails_Id"].ToInt(),
                            Ledger_Id = dr["Ledger_Id"].ToInt(),
                            Rate_Id = dr["ExchangeRate_Id"].ToInt(),
                            Debit = dr["VoucherDetails_Debit"].ToDecimal(),
                            Credit = dr["VoucherDetails_Credit"].ToDecimal(),
                            ChequeNo = dr["VoucherDetails_ChequeNo"].ToString(),
                            ChequeDate = dr["VoucherDetails_ChequeDate"].ToString(),
                            Remark = dr["VoucherDetails_Remark"].ToString(),
                            CostCenter_Id = dr["CostCenter_Id"].ToInt(),
                            Type_Id = dr["VoucherLabel_Id"].ToInt(),
                            RecStatus = dr["VoucherDetails_Status"].ToByte(),
                            Currency_Id = dr["Currency_Id"].ToLong(),
                            Rate = dr["ExchangeRate_Rate"].ToDecimal()
                        });

                    }

                }
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        journalVoucherDto.CurrencyCompanyList.Add(new CurrencyCompany()
                        {
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            Currency_Name = dr["Currency_Name"].ToString(),
                            Rate = dr["Rate"].ToFloat(),
                            ExchangeRate_Id = dr["Currency_Id"].ToLong()
                        });
                    }
                }

                return journalVoucherDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return new JournalVoucherDto();
            }
            finally
            {
                conn.Close();
            }
        }


        public JournalVoucherDto sp_Voucher_Navigate(byte VoucherType_Id, long CurrentvoucherMasters_Id, byte Navigate_Status, out byte Error)
        {

            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Voucher_Navigate", conn);
            command.Parameters.Add(new SqlParameter("@VoucherType_Id", VoucherType_Id));
            command.Parameters.Add(new SqlParameter("@CurrentvoucherMasters_Id", CurrentvoucherMasters_Id));
            command.Parameters.Add(new SqlParameter("@Navigate_Status", Navigate_Status));
            command.Parameters.Add(new SqlParameter("@Error", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;


            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                Error = command.Parameters["@Error"].Value.ToByte();
                JournalVoucherDto journalVoucherDto = new JournalVoucherDto();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    journalVoucherDto.JournalVoucherMasterDto.Id = ds.Tables[0].Rows[0]["voucherMasters_Id"].ToLong();
                    journalVoucherDto.JournalVoucherMasterDto.VoucherNo = ds.Tables[0].Rows[0]["voucherMasters_VoucherNo"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.InvoiceNo = ds.Tables[0].Rows[0]["voucherMasters_InvoiceNo"].ToString();

                    journalVoucherDto.JournalVoucherMasterDto.VoucherDate = ds.Tables[0].Rows[0]["voucherMasters_Date"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.Notes = ds.Tables[0].Rows[0]["voucherMasters_Narration"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.PublicNotes = ds.Tables[0].Rows[0]["voucherMasters_PublicNotes"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.RefNo = ds.Tables[0].Rows[0]["voucherMasters_RefNumber"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.RefNo2 = ds.Tables[0].Rows[0]["voucherMasters_RefNumber2"].ToString();
                    journalVoucherDto.JournalVoucherMasterDto.Branch_Id = ds.Tables[0].Rows[0]["CompanyBranch_Id"].ToInt();
                    journalVoucherDto.JournalVoucherMasterDto.FinancialCycle_Id = ds.Tables[0].Rows[0]["FinancialCycle_Id"].ToInt();
                }
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        journalVoucherDto.JournalVoucherDetailsDto.Add(new JournalVoucherDetailDto()
                        {
                            Id = dr["VoucherDetails_Id"].ToInt(),
                            Ledger_Id = dr["Ledger_Id"].ToInt(),
                            Rate_Id = dr["ExchangeRate_Id"].ToInt(),
                            Debit = dr["VoucherDetails_Debit"].ToDecimal(),
                            Credit = dr["VoucherDetails_Credit"].ToDecimal(),
                            ChequeNo = dr["VoucherDetails_ChequeNo"].ToString(),
                            ChequeDate = dr["VoucherDetails_ChequeDate"].ToString(),
                            Remark = dr["VoucherDetails_Remark"].ToString(),
                            CostCenter_Id = dr["CostCenter_Id"].ToInt(),
                            Type_Id = dr["VoucherLabel_Id"].ToInt(),
                            RecStatus = dr["VoucherDetails_Status"].ToByte(),
                            Currency_Id = dr["Currency_Id"].ToLong(),
                            Rate = dr["ExchangeRate_Rate"].ToDecimal()
                        });

                    }

                }
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        journalVoucherDto.CurrencyCompanyList.Add(new CurrencyCompany()
                        {
                            Currency_Id = dr["Currency_Id"].ToInt(),
                            Currency_Name = dr["Currency_Name"].ToString(),
                            Rate = dr["Rate"].ToFloat(),
                            ExchangeRate_Id = dr["Currency_Id"].ToLong()
                        });
                    }
                }

                return journalVoucherDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                Error = 10;
                return new JournalVoucherDto();
            }
            finally
            {
                conn.Close();
            }
        }
        public ResultJournalVoucher sp_Voucher_Insert(RequestJournalVoucher journalVoucher)
        {

            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Voucher_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@MasterInfo", journalVoucher.MasterInfo));
            command.Parameters.Add(new SqlParameter("@voucherType_Id", journalVoucher.voucherType_Id));
            command.Parameters.Add(new SqlParameter("@Users_Id", journalVoucher.Users_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", journalVoucher.Company_Id));
            command.Parameters.Add(new SqlParameter("@DetailInfo", journalVoucher.DetailInfo));

            command.Parameters.Add(new SqlParameter("@SecondRate", journalVoucher.SecondRate));

            command.Parameters.Add(new SqlParameter("@IsSuccess", SqlDbType.TinyInt)).Direction = ParameterDirection.Output;
            command.Parameters.Add(new SqlParameter("@voucherMasters_Id", SqlDbType.BigInt)).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                ResultJournalVoucher dto = new ResultJournalVoucher()
                {
                    IsSuccess = Convertor.ToByte(command.Parameters["@IsSuccess"].Value),
                    voucherMasters_Id = Convertor.ToInt(command.Parameters["@voucherMasters_Id"].Value)
                };

                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return new ResultJournalVoucher()
                {
                    IsSuccess = 0,
                    voucherMasters_Id = 0
                };
            }
            finally
            {
                conn.Close();
            }
        }


        #endregion
        #region CostCenter
        public CostCenterDto sp_CostCenter_GetById(int company_Id, int costCenter_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_CostCenter_GetById", conn);
            command.Parameters.Add(new SqlParameter("@CostCenter_Id", costCenter_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", company_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                CostCenterDto costCenterDto = new CostCenterDto();

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        costCenterDto.CostCenter_Id = dr["CostCenter_Id"].ToInt();
                        costCenterDto.CostCenter_Name = dr["CostCenter_Name"].ToString();
                        costCenterDto.CostCenter_Description = dr["CostCenter_Description"].ToString();
                        costCenterDto.CostCenter_Status = dr["CostCenter_Status"].ToByte();
                    }

                }
                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                        costCenterDto.CostCenterStatusDto.Add(new CostCenterStatusDto()
                        {
                            Status_Id = dr["Status_Id"].ToByte(),
                            Status_Description = dr["Status_Description"].ToString()
                        });
                }

                return costCenterDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_CostCenter_Insert(int company_Id, CostCenterDto costCenter)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_CostCenter_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Company_Id", company_Id));
            command.Parameters.Add(new SqlParameter("@CostCenter_Id", costCenter.CostCenter_Id));
            command.Parameters.Add(new SqlParameter("@CostCenter_Name", costCenter.CostCenter_Name));
            command.Parameters.Add(new SqlParameter("@CostCenter_Description", costCenter.CostCenter_Description));
            command.Parameters.Add(new SqlParameter("@CostCenter_Status", costCenter.CostCenter_Status));

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<CostCenterListDto> sp_CostCenter_List(int company_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_CostCenter_List", conn);
            command.Parameters.Add(new SqlParameter("@Company_Id", company_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                List<CostCenterListDto> costCenterListDto = new List<CostCenterListDto>();

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        costCenterListDto.Add(new CostCenterListDto()
                        {
                            CostCenter_Id = dr["CostCenter_Id"].ToInt(),
                            CostCenter_Name = dr["CostCenter_Name"].ToString(),
                            Status_Description = dr["Status_Description"].ToString(),
                            CostCenter_Used = dr["CostCenter_Used"].ToBool(),
                        });
                    }
                }

                return costCenterListDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region Project
        public ProjectDto sp_Projects_GetById(int company_Id, int project_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Projects_GetById", conn);
            command.Parameters.Add(new SqlParameter("@Projects_Id", project_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", company_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                ProjectDto projectDto = new ProjectDto();

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        projectDto.Projects_Id = project_Id;
                        projectDto.Projects_Number = dr["Projects_Number"].ToString();
                        projectDto.Projects_Name = dr["Projects_Name"].ToString();
                        projectDto.Projects_Description = dr["Projects_Description"].ToString();
                        projectDto.Projects_StartDate = dr["Projects_StartDate"].ToDateTime();
                        projectDto.Projects_EndDate = dr["Projects_EndDate"].ToDateTime();
                        projectDto.CostCenter_Id = dr["CostCenter_Id"].ToInt();
                        projectDto.Projects_Status = dr["Projects_Status"].ToByte();
                    }
                    if (ds.Tables.Count > 1)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                            projectDto.CostCenterList.Add(new CostCenterDto()
                            {
                                CostCenter_Id = dr["CostCenter_Id"].ToInt(),
                                CostCenter_Name = dr["CostCenter_Name"].ToString(),
                                CostCenter_Status = dr["CostCenter_Status"].ToByte(),
                            });
                    }

                    if (ds.Tables.Count > 2)
                    {
                        foreach (DataRow dr in ds.Tables[2].Rows)
                            projectDto.ProjectStatusList.Add(new ProjectStatusDto()
                            {
                                Status_Id = dr["Status_Id"].ToByte(),
                                Status_Description = dr["Status_Description"].ToString()
                            });
                    }

                }
                return projectDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_Project_Insert(int company_Id, ProjectDto project)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Projects_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Company_Id", company_Id));
            command.Parameters.Add(new SqlParameter("@Projects_Id", project.Projects_Id));
            command.Parameters.Add(new SqlParameter("@Projects_Number", project.Projects_Number));
            command.Parameters.Add(new SqlParameter("@Projects_Name", project.Projects_Name));
            command.Parameters.Add(new SqlParameter("@Projects_Description", project.Projects_Description));
            command.Parameters.Add(new SqlParameter("@Projects_StartDate", project.Projects_StartDate));
            command.Parameters.Add(new SqlParameter("@Projects_EndDate", project.Projects_EndDate));
            command.Parameters.Add(new SqlParameter("@CostCenter_Id", project.CostCenter_Id));
            command.Parameters.Add(new SqlParameter("@Projects_Status", project.Projects_Status));

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<ProjectListDto> sp_Project_List(int company_id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Projects_List", conn);
            command.Parameters.Add(new SqlParameter("@Company_Id", company_id));
            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            try
            {
                conn.Open();
                adapter.Fill(ds);
                List<ProjectListDto> projectListDto = new List<ProjectListDto>();

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        projectListDto.Add(new ProjectListDto()
                        {
                            Projects_Id = dr["Projects_Id"].ToInt(),
                            Projects_Number = dr["Projects_Number"].ToString(),
                            Projects_Name = dr["Projects_Name"].ToString(),
                            Status_Description = dr["Status_Description"].ToString(),
                            Projects_Used = dr["Projects_Used"].ToBool()
                        });
                    }
                }

                return projectListDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_Projects_Delete(int projects_id, out bool Error)
        {
            Error = false;
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Projects_Delete", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Projects_Id", projects_id));
            command.Parameters.Add(new SqlParameter("@CanDelete", SqlDbType.Bit)).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                Error = Convertor.ToBool(command.Parameters["@CanDelete"].Value);
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_CostCenter_Delete(int costcenter_id, out bool Error)
        {
            Error = false;
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_CostCenter_Delete", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@CostCenter_Id", costcenter_id));
            command.Parameters.Add(new SqlParameter("@CanDelete", SqlDbType.Bit)).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                Error = Convertor.ToBool(command.Parameters["@CanDelete"].Value);
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_SuffixPrefix_Insert(SuffixPrefix_Dto dto)
        {

            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_SuffixPrefix_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", dto.Company_Id));
            command.Parameters.Add(new SqlParameter("@SuffixPrefix_FromDate", dto.SuffixPrefix_FromDate));
            command.Parameters.Add(new SqlParameter("@SuffixPrefix_Id", dto.SuffixPrefix_Id));
            command.Parameters.Add(new SqlParameter("@SuffixPrefix_PrefillWithCharacter", dto.SuffixPrefix_PrefillWithCharacter));
            command.Parameters.Add(new SqlParameter("@SuffixPrefix_Prefix", dto.SuffixPrefix_Prefix));
            command.Parameters.Add(new SqlParameter("@SuffixPrefix_StartIndex", dto.SuffixPrefix_StartIndex));

            command.Parameters.Add(new SqlParameter("@SuffixPrefix_Status", dto.SuffixPrefix_Status));
            command.Parameters.Add(new SqlParameter("@SuffixPrefix_Suffix", dto.SuffixPrefix_Suffix));
            command.Parameters.Add(new SqlParameter("@SuffixPrefix_ToDate", dto.SuffixPrefix_ToDate));
            command.Parameters.Add(new SqlParameter("@VoucherType_Id", dto.VoucherType_Id));
            command.Parameters.Add(new SqlParameter("@SuffixPrefix_widthOfNumericalPart", dto.SuffixPrefix_widthOfNumericalPart));
            command.Parameters.Add(new SqlParameter("@narration", dto.Narration));


            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public SuffixPrefix_Dto sp_SuffixPrefix_GetById(int SuffixPrefix_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_SuffixPrefix_GetById", conn);
            command.Parameters.Add(new SqlParameter("@SuffixPrefix_Id", SuffixPrefix_Id));
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());

                if (dt.Rows.Count > 0)
                    return new SuffixPrefix_Dto() { SuffixPrefix_Id = SuffixPrefix_Id, SuffixPrefix_PrefillWithCharacter = dt.Rows[0]["SuffixPrefix_PrefillWithCharacter"].ToString(), SuffixPrefix_FromDate = Convert.ToDateTime(dt.Rows[0]["SuffixPrefix_FromDate"]), SuffixPrefix_ToDate = Convert.ToDateTime(dt.Rows[0]["SuffixPrefix_ToDate"]), SuffixPrefix_Prefix = dt.Rows[0]["SuffixPrefix_Prefix"].ToString(), SuffixPrefix_StartIndex = dt.Rows[0]["SuffixPrefix_StartIndex"].ToInt(), SuffixPrefix_Status = dt.Rows[0]["SuffixPrefix_Status"].ToByte(), SuffixPrefix_Suffix = dt.Rows[0]["SuffixPrefix_Suffix"].ToString(), SuffixPrefix_widthOfNumericalPart = dt.Rows[0]["SuffixPrefix_widthOfNumericalPart"].ToByte(), VoucherType_Id = dt.Rows[0]["VoucherType_Id"].ToInt(), Narration = dt.Rows[0]["Narration"].ToString() };
                return new SuffixPrefix_Dto();
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<SuffixPrefix_Dto> sp_SuffixPrefix_Get(int Company_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_SuffixPrefix_Get", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<SuffixPrefix_Dto> suffixPrefixLst = new List<SuffixPrefix_Dto>();
                int counter = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    suffixPrefixLst.Add(new SuffixPrefix_Dto() { Company_Id = Company_Id, SuffixPrefix_Id = dr["SuffixPrefix_Id"].ToInt(), SuffixPrefix_PrefillWithCharacter = dr["SuffixPrefix_PrefillWithCharacter"].ToString(), SuffixPrefix_FromDate = Convert.ToDateTime(dr["SuffixPrefix_FromDate"]), SuffixPrefix_ToDate = Convert.ToDateTime(dr["SuffixPrefix_ToDate"]), SuffixPrefix_Prefix = dr["SuffixPrefix_Prefix"].ToString(), SuffixPrefix_StartIndex = dr["SuffixPrefix_StartIndex"].ToInt(), SuffixPrefix_Status = dr["SuffixPrefix_Status"].ToByte(), SuffixPrefix_Suffix = dr["SuffixPrefix_Suffix"].ToString(), SuffixPrefix_widthOfNumericalPart = dr["SuffixPrefix_widthOfNumericalPart"].ToByte(), VoucherType_Id = dr["VoucherType_Id"].ToInt(), voucherType_Name = dr["voucherType_Name"].ToString(), No = counter });
                    counter++;
                }
                return suffixPrefixLst;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }



        public List<VoucherTypeDto> sp_SuffixPrefix_Load()
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_SuffixPrefix_Load", conn);
            command.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<VoucherTypeDto> voucherTypeLst = new List<VoucherTypeDto>();
                foreach (DataRow dr in dt.Rows)
                {
                    voucherTypeLst.Add(new VoucherTypeDto() { voucherType_Id = dr["voucherType_Id"].ToInt(), voucherType_Name = dr["voucherType_Name"].ToString() });
                }
                return voucherTypeLst;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region Exchange Rate
        public ExchangeRate_Dto sp_ExchangeRate_GetList(int Company_Id, DateTime? dateTime)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_ExchangeRate_GetList", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@Date", SqlDbType.Date)).Direction = ParameterDirection.InputOutput;
            command.Parameters["@Date"].Value = dateTime;
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                ExchangeRate_Dto exchangerate = new ExchangeRate_Dto();
                exchangerate.VoucherDate = command.Parameters["@Date"].Value.ToString();
                byte counter = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    exchangerate.ExchangeRateList_Dto.Add(new ExchangeRateList_Dto()
                    {
                        Currency_Id = dr["Currency_Id"].ToLong(),
                        Currency_Name = dr["Currency_Name"].ToString(),
                        ExchangeRate_Id = dr["ExchangeRate_Id"].ToLong(),
                        ExchangeRate_Narration = "",
                        No = counter++,
                        Rate = dr["Rate"].ToDecimal(),
                        ExchangeRate_Date = dr["ExchangeRate_Date"].ToDate(),
                        ExchangeRate_Used = dr["ExchangeRate_Used"].ToBool()
                    });
                }
                return exchangerate;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public ExchangeRateInfo_Dto sp_ExchangeRate_GetById(int Company_Id, long exchangerate_id, DateTime datetime)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_ExchangeRate_GetById", conn);
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@ExchangeRate_Id", exchangerate_id));
            command.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            ExchangeRateInfo_Dto exchangerateinfo_dto = new ExchangeRateInfo_Dto();

            try
            {
                conn.Open();
                adapter.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                        exchangerateinfo_dto.ExchangeRateInfoById = new ExchangeRateInfoById_Dto()
                        {
                            ExchangeRate_Id = ds.Tables[0].Rows[0]["ExchangeRate_Id"].ToLong(),
                            Currency_Id = ds.Tables[0].Rows[0]["Currency_Id"].ToLong(),
                            Rate = ds.Tables[0].Rows[0]["ExchangeRate_Rate"].ToDecimal(),
                            ExchangeRate_Date = ds.Tables[0].Rows[0]["ExchangeRate_Date"].ToDate(),
                            ExchangeRate_Narration = ds.Tables[0].Rows[0]["ExchangeRate_Narration"].ToString(),
                        };
                    else
                        exchangerateinfo_dto.ExchangeRateInfoById = new ExchangeRateInfoById_Dto() { ExchangeRate_Date = datetime };
                    if (ds.Tables[1].Rows.Count > 0)
                        foreach (DataRow item in ds.Tables[1].Rows)
                            exchangerateinfo_dto.ExchangeRateInfoDetail_Dto.Add(new ExchangeRateInfoDetail_Dto()
                            {
                                Currency_Id = item["Currency_Id"].ToLong(),
                                Currency_Name = item["Currency_Name"].ToString()
                            });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
            return exchangerateinfo_dto;
        }

        public int sp_ExchangeRate_Insert(ExchangeRateInfoById_Dto exchangerate)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_ExchangeRate_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ExchangeRate_Id", exchangerate.ExchangeRate_Id));
            command.Parameters.Add(new SqlParameter("@Currency_Id", exchangerate.Currency_Id));
            command.Parameters.Add(new SqlParameter("@ExchangeRate_Date", exchangerate.ExchangeRate_Date));
            command.Parameters.Add(new SqlParameter("@ExchangeRate_Rate", exchangerate.Rate));
            command.Parameters.Add(new SqlParameter("@ExchangeRate_Narration", exchangerate.ExchangeRate_Narration));

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_ExchangeRate_Delete(long exchangerate_id, out bool Error)
        {
            Error = false;
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_ExchangeRate_Delete", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ExchangeRate_Id", exchangerate_id));
            command.Parameters.Add(new SqlParameter("@CanDelete", SqlDbType.Bit)).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                Error = Convertor.ToBool(command.Parameters["@CanDelete"].Value);
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region Beneficiary
        public List<BenefeciaryList_Dto> sp_Beneficiary_List(int Company_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Beneficiary_List", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<BenefeciaryList_Dto> benefeciaries = new List<BenefeciaryList_Dto>();
                foreach (DataRow dr in dt.Rows)
                {
                    benefeciaries.Add(new BenefeciaryList_Dto()
                    {
                        Beneficiary_Id = dr["Beneficiary_Id"].ToInt(),
                        Beneficiary_Name = dr["Beneficiary_Name"].ToString(),
                        Beneficiary_Mobile = dr["Beneficiary_Mobile"].ToString(),
                        Beneficiary_Passport = dr["Beneficiary_Passport"].ToString(),
                        Beneficiary_IdNumber = dr["Beneficiary_IdNumber"].ToString()
                    });
                }
                return benefeciaries;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_Beneficiary_Insert(Benefeciary_Dto benefeciary)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Beneficiary_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Beneficiary_Id", benefeciary.Beneficiary_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", benefeciary.Company_Id));
            command.Parameters.Add(new SqlParameter("@Beneficiary_Name", benefeciary.Beneficiary_Name));
            command.Parameters.Add(new SqlParameter("@Beneficiary_Mobile", benefeciary.Beneficiary_Mobile));
            command.Parameters.Add(new SqlParameter("@Beneficiary_Passport", benefeciary.Beneficiary_Passport));
            command.Parameters.Add(new SqlParameter("@Beneficiary_RefNo", benefeciary.Beneficiary_RefNo));
            command.Parameters.Add(new SqlParameter("@Beneficiary_Remark", benefeciary.Beneficiary_Remark));
            command.Parameters.Add(new SqlParameter("@Beneficiary_IdNumber", benefeciary.Beneficiary_IdNumber));

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public int sp_Beneficiary_Delete(int beneficiary_id, out bool Error)
        {
            Error = false;
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Beneficiary_Delete", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Beneficiary_Id", beneficiary_id));
            command.Parameters.Add(new SqlParameter("@CanDelete", SqlDbType.Bit)).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                Error = Convertor.ToBool(command.Parameters["@CanDelete"].Value);
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public Benefeciary_Dto sp_Beneficiary_GetById(int beneficiary_id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Beneficiary_GetById", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Beneficiary_Id", beneficiary_id));
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                Benefeciary_Dto benefeciary = new Benefeciary_Dto();
                if (dt.Rows.Count > 0)
                {
                    benefeciary.Beneficiary_Id = dt.Rows[0]["Beneficiary_Id"].ToInt();
                    benefeciary.Beneficiary_Name = dt.Rows[0]["Beneficiary_Name"].ToString();
                    benefeciary.Beneficiary_Mobile = dt.Rows[0]["Beneficiary_Mobile"].ToString();
                    benefeciary.Beneficiary_Passport = dt.Rows[0]["Beneficiary_Passport"].ToString();
                    benefeciary.Beneficiary_IdNumber = dt.Rows[0]["Beneficiary_IdNumber"].ToString();
                    benefeciary.Beneficiary_Remark = dt.Rows[0]["Beneficiary_Remark"].ToString();
                    benefeciary.Beneficiary_RefNo = dt.Rows[0]["Beneficiary_RefNo"].ToString();
                }
                return benefeciary;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }


        }
        #endregion

        #region Currencies
        public List<CurrencyList_Dto> sp_CurrencyCompany_List(int Company_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_CurrencyCompany_List", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<CurrencyList_Dto> currencies = new List<CurrencyList_Dto>();
                foreach (DataRow dr in dt.Rows)
                {
                    currencies.Add(new CurrencyList_Dto()
                    {
                        Currency_Id = dr["Currency_Id"].ToLong(),
                        Currency_Name = dr["Currency_Name"].ToString(),
                        Currency_Symbol = dr["Currency_Symbol"].ToString()
                    });
                }
                return currencies;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_CurrencyCompany_Insert(Currency_Dto currency)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_CurrencyCompany_Insert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Currency_Id", currency.Currency_Id));
            command.Parameters.Add(new SqlParameter("@Company_Id", currency.Company_Id));
            command.Parameters.Add(new SqlParameter("@Currency_Name", currency.Currency_Name));
            command.Parameters.Add(new SqlParameter("@Currency_Symbol", currency.Currency_Symbol));
            command.Parameters.Add(new SqlParameter("@Currency_Subunit", currency.Currency_Subunit));
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public int sp_CurrencyCompany_Delete(long currency_id, out bool Error)
        {
            Error = false;
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_CurrencyCompany_Delete", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Currency_Id", currency_id));
            command.Parameters.Add(new SqlParameter("@CanDelete", SqlDbType.Bit)).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                Error = Convertor.ToBool(command.Parameters["@CanDelete"].Value);
                return 2;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        public Currency_Dto sp_CurrencyCompany_GetById(long currency_id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_CurrencyCompany_GetById", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Currency_Id", currency_id));
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                Currency_Dto currency = new Currency_Dto();
                if (dt.Rows.Count > 0)
                {
                    currency.Currency_Id = dt.Rows[0]["Currency_Id"].ToLong();
                    currency.Company_Id = dt.Rows[0]["Company_Id"].ToInt();
                    currency.Currency_Name = dt.Rows[0]["Currency_Name"].ToString();
                    currency.Currency_Symbol = dt.Rows[0]["Currency_Symbol"].ToString();
                    currency.Currency_Subunit = dt.Rows[0]["Currency_Subunit"].ToString();
                }
                return currency;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region Remittance
        public List<RemittanceCurrenciesDto> sp_Remittance_GetForInsert(int Company_Id, long Currency_Id)
        {
            SqlConnection conn = new SqlConnection(DumiERPConnectionString);
            SqlCommand command = new SqlCommand("sp_Remittance_GetForInsert", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Company_Id", Company_Id));
            command.Parameters.Add(new SqlParameter("@Currency_Id", Currency_Id));
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                dt.Load(command.ExecuteReader());
                List<RemittanceCurrenciesDto> remittanceCurrencies = new List<RemittanceCurrenciesDto>();

                foreach (DataRow dr in dt.Rows)
                {
                    remittanceCurrencies.Add(new RemittanceCurrenciesDto()
                    {
                        RemittanceSell_Amount = dr["RemittanceSell_Amount"].ToDecimal(),
                        RemittanceSell_AmountInserted = dr["RemittanceSell_AmountInserted"].ToInt(),
                        RemittanceSell_Id = dr["RemittanceSell_Id"].ToInt(),
                        RemmitenceBatch_Id = dr["RemittanceSell_Id"].ToInt(),

                        RemmitenceBatch_Rate = dr["RemmitenceBatch_Rate"].ToDecimal(),
                        RemmitenceBatch_Remaining = dr["RemmitenceBatch_Remaining"].ToDecimal()
                    });
                }
                return remittanceCurrencies;
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.DbErrorOccurred, ex.Message);

                return null;
            }
            finally
            {
                conn.Close();
            }
        }


        #endregion
    }

}
