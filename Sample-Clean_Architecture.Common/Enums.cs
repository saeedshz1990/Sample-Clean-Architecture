using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Common
{
    public class Enums
    {
        public enum UserStatus
        {
            Pending = 0, Confirm = 1
        }
        public enum CompanyUserStatus
        {
            Pending = 0, Active = 1, Disable = 2
        }
        public enum ListType
        {
            CurrencyCompanyList = 0, CompanyBranchList = 1, ProjectList = 3, AccountLedgerList = 4, CostCenterList = 5, VoucherTypeList = 6, DrCr = 7, PaymentReceiptAccountList = 8, VoucherTypes = 9, CurrencyCompanyListWithoutDefault = 10, CurrencyList = 11, CountryList = 12, DefaultLedgerList = 13, DateFormatList = 14, CountryCurrencyList = 15, CurrencySubunitList = 16, ProjectStatusList = 17, CostCenterStatusList = 18, Natures = 19, AccountGroups = 20, ExchangeRateInfoDetail = 21, TermsAndConditionList = 22
        }
    }
}
