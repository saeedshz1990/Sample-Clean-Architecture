using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountGroup;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountLedger;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.AddNewBenefeciary;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Queries.GetBenefeciaries;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompany;
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
using Sample_Clean_Architecture.Application.Services.Users.Queries.GetUserAccesses;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.GetRemittanceForInsert;
using Sample_Clean_Architecture.Application.Services.Vouchers.Remittance.Queries.LoadRemittance;
using Sample_Clean_Architecture.Web.Models.Account;
using Sample_Clean_Architecture.Web.Models.Benefeciary;
using Sample_Clean_Architecture.Web.Models.Companies;
using Sample_Clean_Architecture.Web.Models.CostCenter;
using Sample_Clean_Architecture.Web.Models.Currency;
using Sample_Clean_Architecture.Web.Models.ExchangeRate;
using Sample_Clean_Architecture.Web.Models.Project;
using Sample_Clean_Architecture.Web.Models.Rimittance;
using Sample_Clean_Architecture.Web.Models.SuffixPrefix;
using Sample_Clean_Architecture.Web.Models.UserAccess;
using Sample_Clean_Architecture.Web.Models.Voucher;
using Sample_Clean_Architecture.Web.Utilities;


namespace Sample_Clean_Architecture.Web.Utilities
{
    public static class DtosToModels
    {
        public static IEnumerable<CompanyListModel> CompanyListToModel(List<CompaniesList_Dto> CompaniesDto)
        {
            List<CompanyListModel> companyListModel = new List<CompanyListModel>();
            foreach (CompaniesList_Dto dto in CompaniesDto)
            {
                companyListModel.Add(new CompanyListModel() { CompanyId = dto.Company_Id, AliasName = dto.Company_AliasName, BusinessName = dto.Company_BusinessName, TransactionType = dto.Company_TransactionType });
            }
            return companyListModel;
        }
        public static List<AvalableRateContentModel> AvalableRateContentToModel(List<RemittanceCurrenciesDto> dtos)
        {
            List<AvalableRateContentModel> model = new List<AvalableRateContentModel>();
            foreach (RemittanceCurrenciesDto dto in dtos)
            {
                model.Add(new AvalableRateContentModel() { RemittanceSell_Amount = dto.RemittanceSell_Amount, RemittanceSell_OldAmount = dto.RemittanceSell_Amount, RemittanceSell_AmountInserted = dto.RemittanceSell_AmountInserted, RemittanceSell_Id = dto.RemittanceSell_Id, RemmitenceBatch_Id = dto.RemmitenceBatch_Id, RemmitenceBatch_Rate = dto.RemmitenceBatch_Rate, RemmitenceBatch_Remaining = dto.RemmitenceBatch_Remaining, RecStatus = 0 });
            }
            return model;
        }
        public static List<AvalableRatesContentModel> AvalableRatesContentToModel(List<RemittanceAllCurrencies> dtos)
        {
            List<AvalableRatesContentModel> model = new List<AvalableRatesContentModel>();
            foreach (RemittanceAllCurrencies dto in dtos)
            {
                model.Add(new AvalableRatesContentModel() { CurrencyName = dto.CurrencyName, RemmitenceBatch_Tot = dto.RemmitenceBatch_Tot, RemmitenceBatch_Id = dto.RemmitenceBatch_Id, RemmitenceBatch_Rate = dto.RemmitenceBatch_Rate, RemmitenceBatch_Remaining = dto.RemmitenceBatch_Remaining });
            }
            return model;
        }
        public static AvalableRatesContentModel AvalableRatesContentToModel(RemittanceAllCurrencies dto)
        {
            AvalableRatesContentModel model = new AvalableRatesContentModel() { CurrencyName = dto.CurrencyName, RemmitenceBatch_Tot = dto.RemmitenceBatch_Tot, RemmitenceBatch_Id = dto.RemmitenceBatch_Id, RemmitenceBatch_Rate = dto.RemmitenceBatch_Rate, RemmitenceBatch_Remaining = dto.RemmitenceBatch_Remaining };

            return model;
        }
        public static CompanyFinancialCycleModel CompanyFinancialCycleToModel(CompanyFinancialCycle_Dto dto, string customFormat)
        {
            CompanyFinancialCycleModel model = new CompanyFinancialCycleModel
            {
                Company_Id = dto.Company_Id,
                FinancialCycle_FromDate = dto.FinancialCycle_FromDate.ToCustomFormat(customFormat),
                FinancialCycle_Id = dto.FinancialCycle_Id,
                FinancialCycle_isActive = dto.FinancialCycle_isActive,
                FinancialCycle_Title = dto.FinancialCycle_Title,
                FinancialCycle_ToDate = dto.FinancialCycle_ToDate.ToCustomFormat(customFormat)
            };

            return model;

        }
        public static SuffixPrefixModel SuffixPrefixToModel(Application.Services.SuffixPrefix.Queries.GetSuffixPrefix.SuffixPrefix_Dto dto, string customFormat)
        {
            SuffixPrefixModel model = new SuffixPrefixModel
            {
                Company_Id = dto.Company_Id,
                SuffixPrefix_FromDate = dto.SuffixPrefix_FromDate.ToCustomFormat(customFormat),
                SuffixPrefix_Id = dto.SuffixPrefix_Id,
                SuffixPrefix_PrefillWithCharacter = dto.SuffixPrefix_PrefillWithCharacter,
                SuffixPrefix_Prefix = dto.SuffixPrefix_Prefix,
                SuffixPrefix_StartIndex = dto.SuffixPrefix_StartIndex,
                SuffixPrefix_Status = dto.SuffixPrefix_Status,
                SuffixPrefix_Suffix = dto.SuffixPrefix_Suffix,
                SuffixPrefix_ToDate = dto.SuffixPrefix_ToDate.ToCustomFormat(customFormat),
                VoucherType_Id = dto.VoucherType_Id,
                Narration = dto.Narration,
                voucherType_Name = dto.voucherType_Name,
                No = dto.No.ToString()
            };


            return model;

        }
        public static IEnumerable<SuffixPrefixModel> SuffixPrefixToModel(List<SuffixPrefix_Dto> dtos, string customFormat)
        {
            List<SuffixPrefixModel> lst = new List<SuffixPrefixModel>();
            foreach (SuffixPrefix_Dto dto in dtos)
            {
                lst.Add(SuffixPrefixToModel(dto, customFormat));
            }
            return lst;
        }
        public static CompanyBranchModel CompanyBranchToModel(CompanyBranch_Dto dto)
        {
            CompanyBranchModel model = new CompanyBranchModel
            {
                Company_Id = dto.Company_Id,
                CompanyBranch_Id = dto.CompanyBranch_Id,
                CompanyBranch_Title = dto.CompanyBranch_Title
            };
            return model;

        }
        public static IEnumerable<CompanyBranchModel> CompanyBranchToModel(List<CompanyBranch_Dto> dtos)
        {

            List<CompanyBranchModel> lst = new List<CompanyBranchModel>();
            foreach (CompanyBranch_Dto dto in dtos)
            {
                lst.Add(CompanyBranchToModel(dto));
            }
            return lst;
        }
        public static AccountGroupModel AccountGroupToModel(RequestAccountGroup dto)
        {
            AccountGroupModel model = new AccountGroupModel
            {
                Company_Id = dto.Company_Id,
                AccountGroup_Id = dto.AccountGroup_Id,
                GrossProfit = dto.GrossProfit,
                Name = dto.AccountGroup_Name,
                Narration = dto.AccountGroup_Narration,
                Nature_Id = dto.Nature_Id,
                Parent = dto.AccountGroup_Parent
            };

            return model;
        }
        public static CompanyUserModel CompanyUserToModel(CompanyUserDto dto)
        {
            CompanyUserModel model = new CompanyUserModel
            {
                Company_Id = dto.Company_Id,
                CompanyUsers_Id = dto.CompanyUsers_Id,
                CompanyUserStatus = Enum.GetName(dto.CompanyUsers_Status),
                UserDescription = dto.Users_Description,
                UserName = dto.Users_UserName,
                UserStatus = Enum.GetName(dto.Users_Status)
            };
            return model;
        }

        public static CompanyModel CompanyInfoToModel(RequestCompanyDto dto, string customFormat)
        {
            CompanyModel model = new CompanyModel
            {
                Accounts_Id = dto.Accounts_Id,
                Address = dto.Company_Address,
                AliasName = dto.Company_AliasName,
                BussinessName = dto.Company_BusinessName,
                DateSeperator = dto.Company_DateSeperator,
                Email = dto.Company_Email,
                Fax = dto.Company_Fax,
                CompanyId = dto.Company_Id
            };
            // model.CompanyLogo = dto.Company_Logo;
            if (dto.Company_Logo != null)
            {
                model.CompanyLogo = dto.Company_Logo.GetIFormFile();
                model.CompanyLogoBase64 = Convert.ToBase64String(dto.Company_Logo);
            }
            model.Mobile = dto.Company_Mobile;
            model.PhoneNo = dto.Company_PhoneNo;
            model.PostalCode = dto.Company_PostalCode;
            model.Tax1 = dto.Company_Tax1;
            model.Tax2 = dto.Company_Tax2;
            model.Tax3 = dto.Company_Tax3;
            model.TransactionType = dto.Company_TransactionType;


            model.Web = dto.Company_WebAddress;
            model.Country_Id = dto.Country_Id;

            model.CurrencyName = dto.Currency_Name;

            model.DateFormats_Id = dto.DateFormats_Id;
            model.TransactionStart = dto.FinancialCycle_FromDate.ToCustomFormat(customFormat);

            model.Company_LedgerInserted = dto.Company_LedgerInserted;
            model.DefaultLedgerId = dto.DefaultLedger_Id;
            model.CurrencyAutoupdate = dto.Company_CurrencyAutoupdate;
            model.SubCurrency = dto.Currency_Subunit;
            return model;

        }

        public static IEnumerable<CompanyFinancialCycleModel> CompanyFinancialCycleToModel(List<CompanyFinancialCycle_Dto> companyFinancialCyclesDto, string customFormat)
        {
            List<CompanyFinancialCycleModel> lst = new List<CompanyFinancialCycleModel>();
            foreach (CompanyFinancialCycle_Dto dto in companyFinancialCyclesDto)
            {
                lst.Add(new CompanyFinancialCycleModel() { Company_Id = dto.Company_Id, FinancialCycle_FromDate = dto.FinancialCycle_FromDate.ToCustomFormat(customFormat), FinancialCycle_Id = dto.FinancialCycle_Id, FinancialCycle_isActive = dto.FinancialCycle_isActive, FinancialCycle_Title = dto.FinancialCycle_Title, FinancialCycle_ToDate = dto.FinancialCycle_ToDate.ToCustomFormat(customFormat) });
            }
            return lst;
        }
        public static IEnumerable<CompanyUserModel> CompanyUserToModel(List<CompanyUserDto> dtos)
        {
            List<CompanyUserModel> lst = new List<CompanyUserModel>();
            foreach (CompanyUserDto dto in dtos)
            {
                lst.Add(CompanyUserToModel(dto));
            }
            return lst;
        }


        public static IEnumerable<UserAccessModel> UserAccessToModel(List<ResultMenuDto> dtos)
        {
            List<UserAccessModel> list_menus = new List<UserAccessModel>();

            foreach (ResultMenuDto dto in dtos)
            {
                list_menus.Add(UserAccessToModel(dto));

            }

            return list_menus;
        }


        public static UserAccessModel UserAccessToModel(ResultMenuDto dto)
        {
            UserAccessModel model = new UserAccessModel
            {
                Id = dto.MenuOptions_Id,
                Title = dto.MenuOptions_Title,
                ParentId = dto.MenuOptions_ParentId,
                CompanyUsers_MenuId = dto.CompanyUsers_MenuId
            };
            return model;
        }

        public static AccountModel AccountToModel(AccountLedgerDto dto)
        {
            AccountModel model = new AccountModel
            {
                Ledger_Id = dto.Ledger_Id,
                Ledger_Name = dto.Ledger_Name,
                AccountGroup_Id = dto.AccountGroup_Id,
                Ledger_Code = dto.Ledger_Code,
                Currency_Id = dto.Currency_Id,
                Ledger_BillByBill = dto.Ledger_BillByBill,
                Ledger_Status = dto.Ledger_Status,
                LedgerBank_BankName = dto.LedgerBank_BankName,
                LedgerBank_BranchName = dto.LedgerBank_BranchName,
                LedgerBank_BranchCode = dto.LedgerBank_BranchCode,
                LedgerBank_AccountNumber = dto.LedgerBank_AccountNumber,
                LedgerBank_AccountName = dto.LedgerBank_AccountName,
                LedgerBank_IBAN = dto.LedgerBank_IBAN,
                LedgerBank_Swift = dto.LedgerBank_Swift,
                LedgerBank_HeaderNote = dto.LedgerBank_HeaderNote,
                LedgerBank_FooterNote = dto.LedgerBank_FooterNote,
                LedgerDetails_CreditLimit = dto.LedgerDetails_CreditLimit,
                LedgerDetails_CreditPeriod = dto.LedgerDetails_CreditPeriod,
                LedgerDetails_MailingName = dto.LedgerDetails_MailingName,
                LedgerDetails_Branch = dto.LedgerDetails_Branch,
                LedgerDetails_Email = dto.LedgerDetails_Email,
                LedgerDetails_Address = dto.LedgerDetails_Address,
                LedgerDetails_ContactPerson = dto.LedgerDetails_ContactPerson,
                LedgerDetails_Mobile1 = dto.LedgerDetails_Mobile1,
                LedgerDetails_Mobile2 = dto.LedgerDetails_Mobile2,
                LedgerDetails_Phone = dto.LedgerDetails_Phone,
                LedgerDetails_Fax = dto.LedgerDetails_Fax,
                LedgerDetails_Narration = dto.LedgerDetails_Narration,
                LedgerDetails_BankIBAN = dto.LedgerDetails_BankIBAN,
                LedgerDetails_BankBranchName = dto.LedgerDetails_BankBranchName,
                LedgerDetails_BankBranchCode = dto.LedgerDetails_BankBranchCode,
                LedgerDetails_BankSwiftCode = dto.LedgerDetails_BankSwiftCode,
                LedgerDetails_BankAccountNumber = dto.LedgerDetails_BankAccountNumber,
                LedgerDetails_BankNameOnCheque = dto.LedgerDetails_BankNameOnCheque,
                LedgerDetails_ShipTo = dto.LedgerDetails_ShipTo,
                TermsAndCondition_Id = dto.TermsAndCondition_Id,
                LedgerDetails_CST = dto.LedgerDetails_CST,
                LedgerDetails_TIN = dto.LedgerDetails_TIN,
                LedgerDetails_VAT = dto.LedgerDetails_VAT,
                LedgerDetails_PAN = dto.LedgerDetails_PAN
            };

            return model;
        }

        public static JournalVoucherModel JournalVoucherToModel(JournalVoucherDto dto, string customFormat, int voucherType = 0)
        {
            JournalVoucherModel model = new JournalVoucherModel
            {
                Id = dto.JournalVoucherMasterDto.Id,
                Branch_Id = dto.JournalVoucherMasterDto.Branch_Id,
                Currency_Id = dto.JournalVoucherMasterDto.Currency_Id,
                Notes = dto.JournalVoucherMasterDto.Notes,
                Project_Id = dto.JournalVoucherMasterDto.Project_Id,
                PublicNotes = dto.JournalVoucherMasterDto.PublicNotes,
                RefNo = dto.JournalVoucherMasterDto.RefNo,
                RefNo2 = dto.JournalVoucherMasterDto.RefNo2,
                VoucherDate = Convert.ToDateTime(dto.JournalVoucherMasterDto.VoucherDate).ToCustomFormat(customFormat),
                VoucherNo = dto.JournalVoucherMasterDto.VoucherNo,
                InvoiceNo = dto.JournalVoucherMasterDto.InvoiceNo
            };
            if (voucherType == 1 && dto.JournalVoucherDetailsDto.Count > 0)
            {
                model.PaymentAccount_Id = dto.JournalVoucherDetailsDto[0].Ledger_Id;
                dto.JournalVoucherDetailsDto.RemoveAt(0);
            }
            else if (voucherType == 2 && dto.JournalVoucherDetailsDto.Count > 0)
            {
                model.ReceiptAccount_Id = dto.JournalVoucherDetailsDto[0].Ledger_Id;
                dto.JournalVoucherDetailsDto.RemoveAt(0);
            }
            model.VoucherType = voucherType;

            model.JournalVoucherContentModel = new List<JournalVoucherContentModel>();
            foreach (JournalVoucherDetailDto jvc in dto.JournalVoucherDetailsDto)
            {
                model.JournalVoucherContentModel.Add(new JournalVoucherContentModel()
                {
                    AccountLedger_Id = jvc.Ledger_Id.ToString(),
                    Amount = jvc.Credit > 0 ? jvc.Credit : jvc.Debit,
                    DrCr_Id = jvc.Debit > 0 ? "1" : "2",
                    ChequeDate = Convert.ToDateTime(jvc.ChequeDate).ToCustomFormat(customFormat),
                    ChequeNo = jvc.ChequeNo,
                    CostCenter_Id = jvc.CostCenter_Id.ToString(),
                    Currency_Id = jvc.Currency_Id.ToString(),
                    ExchangeRate = jvc.Rate,
                    ExchangeRateOld = jvc.Rate,
                    Id = jvc.Id,
                    Rate_Id = jvc.Rate_Id.ToString(),
                    RecStatus = jvc.RecStatus,
                    Remark = jvc.Remark,
                    Type_Id = jvc.Type_Id.ToString()
                });
            }

            return model;
        }

        public static BuySellCurrencyModel JournalVoucherToBuySellCurrencyModel(JournalVoucherDto dto, string customFormat, int voucherType = 0)
        {
            BuySellCurrencyModel model = new BuySellCurrencyModel
            {
                Id = dto.JournalVoucherMasterDto.Id,
                Branch_Id = dto.JournalVoucherMasterDto.Branch_Id,
                Currency_Id = dto.JournalVoucherMasterDto.Currency_Id,
                Notes = dto.JournalVoucherMasterDto.Notes,
                Project_Id = dto.JournalVoucherMasterDto.Project_Id,
                PublicNotes = dto.JournalVoucherMasterDto.PublicNotes,
                RefNo = dto.JournalVoucherMasterDto.RefNo,
                RefNo2 = dto.JournalVoucherMasterDto.RefNo2,
                VoucherDate = Convertor.ToDateTime(dto.JournalVoucherMasterDto.VoucherDate).ToCustomFormat(customFormat),
                VoucherNo = dto.JournalVoucherMasterDto.VoucherNo,
                InvoiceNo = dto.JournalVoucherMasterDto.InvoiceNo,
                //if (voucherType == 1 && dto.JournalVoucherDetailsDto.Count > 0)
                //{
                //    model.PaymentAccount_Id = dto.JournalVoucherDetailsDto[0].Ledger_Id;
                //    dto.JournalVoucherDetailsDto.RemoveAt(0);
                //}
                //else if (voucherType == 2 && dto.JournalVoucherDetailsDto.Count > 0)
                //{
                //    model.ReceiptAccount_Id = dto.JournalVoucherDetailsDto[0].Ledger_Id;
                //    dto.JournalVoucherDetailsDto.RemoveAt(0);
                //}
                VoucherType = voucherType,

                JournalVoucherContentModel = new List<JournalVoucherContentModel>()
            };
            if (dto.JournalVoucherDetailsDto.Count > 0)
            {
                JournalVoucherDetailDto buyingDto = dto.JournalVoucherDetailsDto[0];
                model.LedgerAccount_Id = buyingDto.Ledger_Id;
                model.BuyingCurrency_Id = buyingDto.Currency_Id;
                model.BuyingExchangeRate = buyingDto.Rate;
                model.BuyingExchangeRateOld = buyingDto.Rate;
                model.BuyingAmount = buyingDto.Debit;
                model.BuyingRemark = buyingDto.Remark;
                //model.TMN_AED=
            }
            if (dto.JournalVoucherDetailsDto.Count > 1)
            {
                JournalVoucherDetailDto sellingDto = dto.JournalVoucherDetailsDto[1];
                model.LedgerAccount_Id = sellingDto.Ledger_Id;
                model.SellingCurrency_Id = sellingDto.Currency_Id;
                model.SellingExchangeRate = sellingDto.Rate;
                model.SellingExchangeRateOld = sellingDto.Rate;
                model.SellingAmount = sellingDto.Credit;
                model.SellingRemark = sellingDto.Remark;
            }
            //foreach (JournalVoucherDetailDto jvc in dto.JournalVoucherDetailsDto)
            for (int i = 2; i < dto.JournalVoucherDetailsDto.Count; i++)
            {
                model.JournalVoucherContentModel.Add(new JournalVoucherContentModel()
                {
                    AccountLedger_Id = dto.JournalVoucherDetailsDto[i].Ledger_Id.ToString(),
                    Amount = dto.JournalVoucherDetailsDto[i].Credit > 0 ? dto.JournalVoucherDetailsDto[i].Credit : dto.JournalVoucherDetailsDto[i].Debit,
                    DrCr_Id = dto.JournalVoucherDetailsDto[i].Debit > 0 ? "1" : "2",
                    Currency_Id = dto.JournalVoucherDetailsDto[i].Currency_Id.ToString(),
                    ExchangeRate = dto.JournalVoucherDetailsDto[i].Rate,
                    ExchangeRateOld = dto.JournalVoucherDetailsDto[i].Rate,
                    Id = dto.JournalVoucherDetailsDto[i].Id,
                    Rate_Id = dto.JournalVoucherDetailsDto[i].Rate_Id.ToString(),
                    RecStatus = dto.JournalVoucherDetailsDto[i].RecStatus,
                    Remark = dto.JournalVoucherDetailsDto[i].Remark,
                    Type_Id = dto.JournalVoucherDetailsDto[i].Type_Id.ToString()
                });
            }

            return model;
        }



        public static ExchangeRateModel ExchangeRateToModel(ExchangeRate_Dto dto, string customFormat)

        {
            ExchangeRateModel model = new ExchangeRateModel();
            model.VoucherDate = Convert.ToDateTime(dto.VoucherDate).ToCustomFormat(customFormat);
            model.ExchangeRateListModel = new List<ExchangeRateListModel>();
            foreach (var item in dto.ExchangeRateList_Dto)
            {
                model.ExchangeRateListModel.Add(new ExchangeRateListModel()
                {
                    //Currency_Id = item.Currency_Id,
                    ExchangeRate_Id = item.ExchangeRate_Id,
                    No = item.No,
                    Currency_Name = item.Currency_Name,
                    ExchangeRate_Date = item.ExchangeRate_Date.ToCustomFormat(customFormat),
                    Rate = item.Rate,
                    ExchangeRate_Used = item.ExchangeRate_Used
                });
            }
            return model;
        }

        public static BenefeciaryModel BenefeciaryToModel(BenefeciaryList_Dto dto)
        {
            BenefeciaryModel model = new BenefeciaryModel();
            model.Beneficiary_Id = dto.Beneficiary_Id;
            model.Beneficiary_Name = dto.Beneficiary_Name;
            model.Beneficiary_Mobile = dto.Beneficiary_Mobile;
            model.Beneficiary_Passport = dto.Beneficiary_Passport;
            model.Beneficiary_IdNumber = dto.Beneficiary_IdNumber;
            return model;
        }

        public static CurrencyModel CurrencyToModel(CurrencyList_Dto dto)
        {
            CurrencyModel model = new CurrencyModel();
            model.Currency_Id = dto.Currency_Id;
            model.Currency_Name = dto.Currency_Name;
            model.Currency_Symbol = dto.Currency_Symbol;
            return model;
        }

        public static CurrencyInfoModel CurrencyInfoToModel(Currency_Dto dto)
        {
            CurrencyInfoModel model = new CurrencyInfoModel();
            model.Currency_Id = dto.Currency_Id;
            model.Currency_Name = dto.Currency_Name;
            model.Company_Id = dto.Company_Id;
            model.Currency_Symbol = dto.Currency_Symbol;
            model.Currency_Subunit = dto.Currency_Subunit;
            return model;
        }

        public static IEnumerable<CurrencyModel> CurrencyToModel(List<CurrencyList_Dto> dtos)
        {
            List<CurrencyModel> lst = new List<CurrencyModel>();
            foreach (CurrencyList_Dto dto in dtos)
            {
                lst.Add(CurrencyToModel(dto));
            }
            return lst;
        }

        public static IEnumerable<BenefeciaryModel> BenefeciaryToModel(List<BenefeciaryList_Dto> dtos)
        {
            List<BenefeciaryModel> lst = new List<BenefeciaryModel>();
            foreach (BenefeciaryList_Dto dto in dtos)
            {
                lst.Add(BenefeciaryToModel(dto));
            }
            return lst;
        }

        public static BenefeciaryInfoModel BenefeciaryInfoToModel(Benefeciary_Dto dto)
        {
            BenefeciaryInfoModel model = new BenefeciaryInfoModel();
            model.Beneficiary_Id = dto.Beneficiary_Id;

            model.Beneficiary_Name = dto.Beneficiary_Name;
            model.Beneficiary_Mobile = dto.Beneficiary_Mobile;
            model.Beneficiary_Passport = dto.Beneficiary_Passport;
            model.Beneficiary_IdNumber = dto.Beneficiary_IdNumber;
            model.Beneficiary_RefNo = dto.Beneficiary_RefNo;
            model.Beneficiary_Remark = dto.Beneficiary_Remark;
            return model;
        }

        public static ExchangeRateInfoModel ExchangeRateInfoToModel(ExchangeRateInfoById_Dto dto, string customFormat)
        {
            ExchangeRateInfoModel model = new ExchangeRateInfoModel();
            model.ExchangeRate_Id = dto.ExchangeRate_Id;
            model.Currency_Id = dto.Currency_Id;
            model.ExchangeRate_Date = dto.ExchangeRate_Date.ToCustomFormat(customFormat);// dto.ExchangeRate_Date.ToCustomFormat(customFormat);
            model.Rate = dto.Rate;
            model.ExchangeRate_Narration = dto.ExchangeRate_Narration;
            model.No = 0;
            return model;
        }

        public static ProjectInfoModel ProjectInfoToModel(ProjectDto dto, string customFormat)
        {
            ProjectInfoModel model = new ProjectInfoModel
            {
                Projects_Id = dto.Projects_Id,
                Projects_Number = dto.Projects_Number,
                Projects_Name = dto.Projects_Name,
                Projects_Status = dto.Projects_Status,
                Projects_Description = dto.Projects_Description,
                Projects_StartDate = dto.Projects_StartDate.ToCustomFormat(customFormat),
                Projects_EndDate = dto.Projects_EndDate.ToCustomFormat(customFormat),
                CostCenter_Id = dto.CostCenter_Id
            };
            return model;
        }

        public static CostCenterInfoModel CostCenterInfoToModel(CostCenterDto dto)
        {
            CostCenterInfoModel model = new CostCenterInfoModel();
            model.CostCenter_Id = dto.CostCenter_Id;
            model.CostCenter_Name = dto.CostCenter_Name;
            model.CostCenter_Status = dto.CostCenter_Status;
            model.CostCenter_Description = dto.CostCenter_Description;
            return model;
        }


        public static ProjectModel ProjectToModel(ProjectListDto dto)
        {
            ProjectModel model = new ProjectModel();
            model.Projects_Id = dto.Projects_Id;
            model.Projects_Number = dto.Projects_Number;
            model.Projects_Name = dto.Projects_Name;
            model.Status_Description = dto.Status_Description;
            model.Projects_Used = dto.Projects_Used;
            return model;
        }

        public static CostCenterModel CostCenterToModel(CostCenterListDto dto)
        {
            CostCenterModel model = new CostCenterModel();
            model.CostCenter_Id = dto.CostCenter_Id;
            model.CostCenter_Name = dto.CostCenter_Name;
            model.Status_Description = dto.Status_Description;
            model.CostCenter_Used = dto.CostCenter_Used;
            return model;
        }
        public static IEnumerable<CostCenterModel> CostCenterToModel(List<CostCenterListDto> dtos)
        {
            List<CostCenterModel> lst = new List<CostCenterModel>();
            foreach (CostCenterListDto dto in dtos)
            {
                lst.Add(CostCenterToModel(dto));
            }
            return lst;
        }

        public static IEnumerable<ProjectModel> ProjectToModel(List<ProjectListDto> dtos)
        {
            List<ProjectModel> lst = new List<ProjectModel>();
            foreach (ProjectListDto dto in dtos)
            {
                lst.Add(ProjectToModel(dto));
            }
            return lst;
        }

        /*
                public static IEnumerable<ExchangeRateModel> ExchangeRateToModel(List<ExchangeRate_Dto> dtos)
                {
                    List<ExchangeRateModel> lst = new List<ExchangeRateModel>();
                    foreach (ExchangeRate_Dto dto in dtos)
                    {
                        lst.Add(ExchangeRateToModel(dto));
                    }
                    return lst;
                }*/
    }



}
