using Sample_Clean_Architecture.Web.Models.Voucher;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Rimittance
{
    public class BuySellCurrencyModel : JournalVoucherModel
    {
        [DisplayName("Customer A/C")]
        public long LedgerAccount_Id { set; get; }
        [DisplayName("Buying(DR)")]
        public long BuyingCurrency_Id { set; get; }

        [DisplayName("Selling(CR)")]
        public long BuyingCurrency_IdLable { set; get; }

        [DisplayName("Selling(CR)")]
        public long SellingCurrency_Id { set; get; }

        [DisplayName("Buying(DR)")]
        public long SellingCurrency_IdLable { set; get; }

        [DisplayName("Amount")]
        public decimal BuyingAmount { set; get; }
        [DisplayName("Amount")]
        public decimal SellingAmount { set; get; }
        [DisplayName("Rate")]
        public decimal BuyingExchangeRate { set; get; }
        [DisplayName("Rate")]
        public decimal SellingExchangeRate { set; get; }

        [DisplayName("Remark")]
        public string BuyingRemark { set; get; }
        [DisplayName("Remark")]
        public string SellingRemark { set; get; }

        [DisplayName("Type")]
        public string BuyingType { set; get; }
        [DisplayName("Type")]
        public string SellingType { set; get; }

        public decimal BuyingExchangeRateOld { set; get; }

        public decimal SellingExchangeRateOld { set; get; }
        public byte Remittance_AmountDecimalNo { set; get; }
        public decimal Remittance_BalanceDifference { set; get; }
        public long Remittance_BalanceDifferenceLedger { set; get; }
        public byte Remittance_RateDecimalNo { set; get; }
        public List<AvalableRateContentModel> AvalableRateContentModel { set; get; }
        public List<AvalableRatesContentModel> AvalableRatesContentModel { set; get; }
        public BuySellCurrencyModel()
        {
            AvalableRateContentModel = new List<AvalableRateContentModel>();
            AvalableRatesContentModel = new List<AvalableRatesContentModel>();
        }
    }
    public class AvalableRateContentModel
    {
        [Key]
        public int RemmitenceBatch_Id { set; get; }
        [DisplayName("Rate")]
        public decimal RemmitenceBatch_Rate { set; get; }
        [DisplayName("Remaining")]
        public decimal RemmitenceBatch_Remaining { set; get; }
        [DisplayName("Used Amount")]
        public decimal RemittanceSell_Amount { set; get; }
        public decimal RemittanceSell_OldAmount { set; get; }
        public int RemittanceSell_Id { set; get; }
        public int RemittanceSell_AmountInserted { set; get; }
        public byte RecStatus { set; get; }
    }
    public class AvalableRatesContentModel
    {
        [Key]
        public int RemmitenceBatch_Id { set; get; }
        [DisplayName("Currency")]
        public string CurrencyName { set; get; }
        [DisplayName("Rate")]
        public decimal RemmitenceBatch_Rate { set; get; }
        [DisplayName("Remaining")]
        public decimal RemmitenceBatch_Remaining { set; get; }
        [DisplayName("Total")]
        public decimal RemmitenceBatch_Tot { set; get; }

    }
}
