using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountGroup;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountLedger;
using Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.AddNewBenefeciary;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompany;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyBranches;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyFinancialCycle;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyUsers;
using Sample_Clean_Architecture.Application.Services.CostCenter.Commands.AddNewCostCenter;
using Sample_Clean_Architecture.Application.Services.Currencies.Queries.GetCurrencies;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Queries.GetExchangeRate;
using Sample_Clean_Architecture.Application.Services.Project.Commands.AddNewProject;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Commands.AddNewJournalVoucher;
using Sample_Clean_Architecture.Web.Models.Account;
using Sample_Clean_Architecture.Web.Models.Benefeciary;
using Sample_Clean_Architecture.Web.Models.Companies;
using Sample_Clean_Architecture.Web.Models.CostCenter;
using Sample_Clean_Architecture.Web.Models.Currency;
using Sample_Clean_Architecture.Web.Models.ExchangeRate;
using Sample_Clean_Architecture.Web.Models.Project;
using Sample_Clean_Architecture.Web.Models.SuffixPrefix;
using Sample_Clean_Architecture.Web.Models.Voucher;
using static Sample_Clean_Architecture.Common.Enums;

namespace Sample_Clean_Architecture.Web.Utilities
{
    public static class ModelsToDtos
    {
        public static RequestCompanyDto CompanyToDto(CompanyModel model, string customFormat)
        {
            RequestCompanyDto dto = new RequestCompanyDto
            {
                Accounts_Id = model.Accounts_Id,
                Company_Address = model.Address,
                Company_AliasName = model.AliasName,
                Company_BusinessName = model.BussinessName,
                Company_DateSeperator = model.DateSeperator,
                Company_Email = model.Email,
                Company_Fax = model.Fax,
                Company_Id = model.CompanyId
            };
            // dto.Company_Logo = model.CompanyLogo;
            if (model.CompanyLogo != null)
                dto.Company_Logo = model.CompanyLogo.GetBytes();
            else
                dto.Company_Logo = new byte[1] { 0x20 };

            dto.Company_Mobile = model.Mobile;
            dto.Company_PhoneNo = model.PhoneNo;
            dto.Company_PostalCode = model.PostalCode;
            dto.Company_Tax1 = model.Tax1;
            dto.Company_Tax2 = model.Tax2;
            dto.Company_Tax3 = model.Tax3;
            dto.Company_TransactionType = model.TransactionType;

            dto.Company_WebAddress = model.Web;
            dto.Country_Id = model.Country_Id;
            dto.Country_Currency_Id = model.Country_Currency_Id;
            dto.Currency_Subunit = model.SubCurrency;
            dto.Currency_Name = model.CurrencyName;
            dto.DateFormats_Id = model.DateFormats_Id;
            dto.FinancialCycle_FromDate = model.TransactionStart.ToSystemFormat(customFormat).ToDate();
            dto.Company_LedgerInserted = model.Company_LedgerInserted;
            dto.DefaultLedger_Id = model.DefaultLedgerId;
            dto.Company_CurrencyAutoupdate = model.CurrencyAutoupdate;
            return dto;
        }

        public static CompanyFinancialCycle_Dto FinancialCycleToDto(CompanyFinancialCycleModel model, string customFormat)
        {
            CompanyFinancialCycle_Dto dto = new CompanyFinancialCycle_Dto();
            dto.Company_Id = model.Company_Id;
            dto.FinancialCycle_FromDate = model.FinancialCycle_FromDate.ToSystemFormat(customFormat).ToDate(); ;
            dto.FinancialCycle_Id = model.FinancialCycle_Id;
            dto.FinancialCycle_isActive = model.FinancialCycle_isActive;
            dto.FinancialCycle_Title = model.FinancialCycle_Title;
            dto.FinancialCycle_ToDate = model.FinancialCycle_ToDate.ToSystemFormat(customFormat).ToDate(); ;
            return dto;
        }
        public static Benefeciary_Dto BenefeciaryInfoToDto(BenefeciaryInfoModel model)
        {
            Benefeciary_Dto dto = new Benefeciary_Dto();
            dto.Company_Id = model.Company_Id;
            dto.Beneficiary_Id = model.Beneficiary_Id;
            dto.Beneficiary_Name = model.Beneficiary_Name;
            dto.Beneficiary_Mobile = model.Beneficiary_Mobile;
            dto.Beneficiary_Passport = model.Beneficiary_Passport;
            dto.Beneficiary_RefNo = model.Beneficiary_RefNo;
            dto.Beneficiary_Remark = model.Beneficiary_Remark;
            dto.Beneficiary_IdNumber = model.Beneficiary_IdNumber;
            return dto;
        }

        public static ExchangeRateInfoById_Dto ExchangeRateInfoToDto(ExchangeRateInfoModel model, string customformat)
        {
            ExchangeRateInfoById_Dto dto = new ExchangeRateInfoById_Dto();
            dto.ExchangeRate_Id = model.ExchangeRate_Id;
            dto.Currency_Id = model.Currency_Id;
            dto.ExchangeRate_Date = model.ExchangeRate_Date.ToSystemFormat(customformat).ToDateTime();
            dto.Rate = model.Rate;
            dto.ExchangeRate_Narration = model.ExchangeRate_Narration;
            return dto;
        }

        public static ProjectDto ProjectInfoToDto(ProjectInfoModel model, string customformat)
        {
            ProjectDto dto = new ProjectDto();
            dto.Projects_Id = model.Projects_Id;
            dto.Projects_Number = model.Projects_Number;
            dto.Projects_Name = model.Projects_Name;
            dto.Projects_Description = model.Projects_Description;
            dto.Projects_StartDate = model.Projects_StartDate.ToSystemFormat(customformat).ToDateTime();
            dto.Projects_EndDate = model.Projects_EndDate.ToSystemFormat(customformat).ToDateTime();
            dto.CostCenter_Id = model.CostCenter_Id;
            dto.Projects_Status = model.Projects_Status;
            return dto;
        }

        public static CostCenterDto CostCenterInfoToDto(CostCenterInfoModel model)
        {
            CostCenterDto dto = new CostCenterDto();
            dto.CostCenter_Id = model.CostCenter_Id;
            dto.CostCenter_Name = model.CostCenter_Name;
            dto.CostCenter_Status = model.CostCenter_Status;
            dto.CostCenter_Description = model.CostCenter_Description;
            return dto;
        }



        public static Currency_Dto CurrencyInfoToDto(CurrencyInfoModel model)
        {
            Currency_Dto dto = new Currency_Dto();
            dto.Company_Id = model.Company_Id;
            dto.Currency_Id = model.Currency_Id;
            dto.Currency_Name = model.Currency_Name;
            dto.Currency_Symbol = model.Currency_Symbol;
            dto.Currency_Subunit = model.Currency_Subunit;
            return dto;
        }



        public static SuffixPrefix_Dto SuffixPrefixToDto(SuffixPrefixModel model, string customformat)
        {
            SuffixPrefix_Dto dto = new SuffixPrefix_Dto();

            dto.Company_Id = model.Company_Id;
            dto.SuffixPrefix_FromDate = model.SuffixPrefix_FromDate.ToSystemFormat(customformat).ToDateTime(); ;
            dto.SuffixPrefix_Id = model.SuffixPrefix_Id;
            dto.SuffixPrefix_PrefillWithCharacter = model.SuffixPrefix_PrefillWithCharacter;
            dto.SuffixPrefix_Prefix = model.SuffixPrefix_Prefix;
            dto.SuffixPrefix_StartIndex = model.SuffixPrefix_StartIndex;
            dto.SuffixPrefix_Status = model.SuffixPrefix_Status;
            dto.SuffixPrefix_Suffix = model.SuffixPrefix_Suffix;
            dto.SuffixPrefix_ToDate = model.SuffixPrefix_ToDate.ToSystemFormat(customformat).ToDateTime(); ;
            dto.VoucherType_Id = model.VoucherType_Id;
            dto.Narration = model.Narration;
            return dto;
        }
        public static CompanyBranch_Dto CompanyBranchToDto(CompanyBranchModel model)
        {
            CompanyBranch_Dto dto = new CompanyBranch_Dto();
            dto.Company_Id = model.Company_Id;
            dto.CompanyBranch_Id = model.CompanyBranch_Id;
            dto.CompanyBranch_Title = model.CompanyBranch_Title;

            return dto;
        }
        public static RequestAccountGroup AccountGroupToDto(AccountGroupModel model)
        {
            RequestAccountGroup dto = new RequestAccountGroup();
            dto.Company_Id = model.Company_Id;
            dto.AccountGroup_Id = model.AccountGroup_Id;
            dto.AccountGroup_Name = model.Name;
            dto.AccountGroup_Narration = model.Narration;
            dto.AccountGroup_Parent = model.Parent;
            dto.GrossProfit = model.GrossProfit;
            dto.Nature_Id = model.Nature_Id;


            return dto;
        }
        public static AccountLedgerDto AccountToDto(AccountModel model)
        {
            AccountLedgerDto dto = new AccountLedgerDto();
            dto.Company_Id = model.Company_Id;
            dto.Ledger_Id = model.Ledger_Id;
            dto.Ledger_Name = model.Ledger_Name;
            dto.AccountGroup_Id = model.AccountGroup_Id;
            dto.Ledger_Code = model.Ledger_Code;
            dto.Currency_Id = model.Currency_Id;
            dto.Ledger_BillByBill = model.Ledger_BillByBill;
            dto.Ledger_Status = model.Ledger_Status;
            //Bank
            dto.LedgerBank_BankName = model.LedgerBank_BankName;
            dto.LedgerBank_BranchName = model.LedgerBank_BranchName;
            dto.LedgerBank_BranchCode = model.LedgerBank_BranchCode;
            dto.LedgerBank_AccountNumber = model.LedgerBank_AccountNumber;
            dto.LedgerBank_AccountName = model.LedgerBank_AccountName;
            dto.LedgerBank_IBAN = model.LedgerBank_IBAN;
            dto.LedgerBank_Swift = model.LedgerBank_Swift;
            dto.LedgerBank_HeaderNote = model.LedgerBank_HeaderNote;
            dto.LedgerBank_FooterNote = model.LedgerBank_FooterNote;
            //Bank
            //Details
            dto.LedgerDetails_CreditLimit = model.LedgerDetails_CreditLimit;
            dto.LedgerDetails_CreditPeriod = model.LedgerDetails_CreditPeriod;
            dto.LedgerDetails_MailingName = model.LedgerDetails_MailingName;
            dto.LedgerDetails_Branch = model.LedgerDetails_Branch;
            dto.LedgerDetails_Email = model.LedgerDetails_Email;
            dto.LedgerDetails_Address = model.LedgerDetails_Address;
            dto.LedgerDetails_ContactPerson = model.LedgerDetails_ContactPerson;
            dto.LedgerDetails_Mobile1 = model.LedgerDetails_Mobile1;
            dto.LedgerDetails_Mobile2 = model.LedgerDetails_Mobile2;
            dto.LedgerDetails_Phone = model.LedgerDetails_Phone;
            dto.LedgerDetails_Fax = model.LedgerDetails_Fax;
            dto.LedgerDetails_Narration = model.LedgerDetails_Narration;
            dto.LedgerDetails_BankIBAN = model.LedgerDetails_BankIBAN;
            dto.LedgerDetails_BankBranchName = model.LedgerDetails_BankBranchName;
            dto.LedgerDetails_BankBranchCode = model.LedgerDetails_BankBranchCode;
            dto.LedgerDetails_BankSwiftCode = model.LedgerDetails_BankSwiftCode;
            dto.LedgerDetails_BankAccountNumber = model.LedgerDetails_BankAccountNumber;
            dto.LedgerDetails_BankNameOnCheque = model.LedgerDetails_BankNameOnCheque;
            dto.LedgerDetails_ShipTo = model.LedgerDetails_ShipTo;
            dto.TermsAndCondition_Id = model.TermsAndCondition_Id;
            dto.LedgerDetails_CST = model.LedgerDetails_CST;
            dto.LedgerDetails_TIN = model.LedgerDetails_TIN;
            dto.LedgerDetails_VAT = model.LedgerDetails_VAT;
            dto.LedgerDetails_PAN = model.LedgerDetails_PAN;
            //Details


            return dto;
        }

        public static CompanyUserDto CompanyUserToDto(CompanyUserModel model)
        {
            CompanyUserDto dto = new CompanyUserDto();
            dto.Company_Id = model.Company_Id;
            dto.CompanyUsers_Id = model.CompanyUsers_Id;
            dto.CompanyUsers_Status = (CompanyUserStatus)Enum.Parse(typeof(CompanyUserStatus), model.CompanyUserStatus);
            dto.Users_Description = model.UserDescription;
            dto.Users_UserName = model.UserName;
            dto.Users_Status = (UserStatus)Enum.Parse(typeof(UserStatus), model.UserStatus);
            dto.Users_Password = model.Password;
            return dto;
        }
        public static JournalVoucherDto JournalVoucherToDto(JournalVoucherModel model, string customFormat)
        {
            JournalVoucherDto dto = new JournalVoucherDto();
            dto.JournalVoucherMasterDto.Id = model.Id;
            dto.JournalVoucherMasterDto.Branch_Id = model.Branch_Id;
            dto.JournalVoucherMasterDto.Currency_Id = model.Currency_Id;
            dto.JournalVoucherMasterDto.Notes = model.Notes;
            dto.JournalVoucherMasterDto.Project_Id = model.Project_Id;
            dto.JournalVoucherMasterDto.PublicNotes = model.PublicNotes;
            dto.JournalVoucherMasterDto.RefNo = model.RefNo;
            dto.JournalVoucherMasterDto.RefNo2 = model.RefNo2;
            dto.JournalVoucherMasterDto.VoucherDate = model.VoucherDate.ToSystemFormat(customFormat);
            dto.JournalVoucherMasterDto.VoucherNo = model.VoucherNo;
            dto.JournalVoucherMasterDto.InvoiceNo = model.InvoiceNo;


            foreach (JournalVoucherContentModel jvc in model.JournalVoucherContentModel)
            {
                dto.JournalVoucherDetailsDto.Add(new JournalVoucherDetailDto()
                {
                    Ledger_Id = jvc.AccountLedger_Id.ToLong(),
                    Debit = jvc.DrCr_Id == "1" ? jvc.Amount : 0,
                    Credit = jvc.DrCr_Id == "2" ? jvc.Amount : 0,
                    ChequeDate = jvc.ChequeDate.ToSystemFormat(customFormat),
                    ChequeNo = jvc.ChequeNo,
                    CostCenter_Id = jvc.CostCenter_Id.ToInt(),
                    Currency_Id = jvc.Currency_Id.ToLong(),
                    Rate = jvc.ExchangeRate,
                    Id = jvc.Id,
                    Rate_Id = jvc.Rate_Id.ToLong(),
                    RecStatus = jvc.RecStatus.ToByte(),
                    Remark = jvc.Remark,
                    Type_Id = jvc.Type_Id.ToInt(),
                    Ratechnage = !(jvc.ExchangeRate == jvc.ExchangeRateOld)
                });
            }

            return dto;
        }

        //public static JournalVoucherDto OtherVoucherToDto(OtherVoucherModel model, string customFormat)
        //{
        //    JournalVoucherDto dto = new JournalVoucherDto();
        //    dto.JournalVoucherMasterDto.Id = model.Id;
        //    dto.JournalVoucherMasterDto.Branch_Id = model.Branch_Id;
        //    dto.JournalVoucherMasterDto.Currency_Id = model.Currency_Id;
        //   // dto.JournalVoucherMasterDto.Notes = model.Notes;
        //    dto.JournalVoucherMasterDto.Project_Id = model.Project_Id;
        //    dto.JournalVoucherMasterDto.PublicNotes = model.Narration;
        //    dto.JournalVoucherMasterDto.RefNo = model.RefNo;
        //    dto.JournalVoucherMasterDto.RefNo2 = model.RefNo2;
        //    dto.JournalVoucherMasterDto.VoucherDate = model.VoucherDate.ToSystemFormat(customFormat);
        //    dto.JournalVoucherMasterDto.VoucherNo = model.VoucherNo;

        //    foreach (OtherVoucherContentModel jvc in model.OtherVoucherContentModel)
        //    {
        //        dto.JournalVoucherDetailsDto.Add(new JournalVoucherDetailDto()
        //        {
        //            Ledger_Id = jvc.AccountLedger_Id.ToLong(),
        //            Debit = (jvc.DrCr_Id == "1") ? jvc.Amount : 0,
        //            Credit = (jvc.DrCr_Id == "2") ? jvc.Amount : 0,
        //            ChequeDate = jvc.ChequeDate.ToSystemFormat(customFormat),
        //            ChequeNo = jvc.ChequeNo,
        //            CostCenter_Id = jvc.CostCenter_Id.ToInt(),
        //            Currency_Id = jvc.Currency_Id.ToLong(),
        //            Rate = jvc.ExchangeRate,
        //            Id = jvc.Id,
        //            Rate_Id = jvc.Rate_Id.ToLong(),
        //            RecStatus = jvc.RecStatus.ToByte(),
        //            Remark = jvc.Remark,
        //            Type_Id = jvc.Type_Id.ToInt(),
        //            Ratechnage = !(jvc.ExchangeRate == jvc.ExchangeRateOld)
        //        });
        //    }

        //    return dto;
        //}
    }
}
