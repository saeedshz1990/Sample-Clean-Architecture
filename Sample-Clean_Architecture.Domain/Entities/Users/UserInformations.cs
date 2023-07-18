using Sample_Clean_Architecture.Domain.Entities.Commons;
using System.Collections.Generic;

namespace Sample_Clean_Architecture.Domain.Entities.Users
{
    public class UserInformations : User
    {
        public List<UserBusiness> UserInBusiness { get; set; }
        public List<Menu> Menus { get; set; }
        public byte TransactionType { get; set; }
        public byte DateFormats_Id { get; set; }
        public byte CompanyDateSeperator { get; set; }
        public DateTime FinancialCycle_FromDate { get; set; }
        public DateTime FinancialCycle_ToDate { get; set; }
        public int Accounts_Id { get; set; }
    }
}
