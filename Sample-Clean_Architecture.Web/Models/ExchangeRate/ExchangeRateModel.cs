using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.ExchangeRate
{
    public class ExchangeRateModel
    {
        [DisplayName("Date")]
        //[BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        // [BindProperty, DataType(DataType.Date) ,Localizable(true)]
        public string VoucherDate { set; get; }
        public List<ExchangeRateListModel> ExchangeRateListModel { set; get; }

        //public ExchangeRateModel()
        //{
        //    ExchangeRateListModel = new List<ExchangeRateListModel>();
        //}
    }

    public class ExchangeRateListModel
    {
        [Key]
        public long ExchangeRate_Id { get; set; }
        //public long Currency_Id { get; set; }
        [DisplayName("No")]
        public byte No { get; set; }
        [DisplayName("Currency Name")]
        public string Currency_Name { get; set; }

        [DisplayName("Rate")]
        public decimal Rate { get; set; }
        [DisplayName("Date")]
        public string ExchangeRate_Date { get; set; }
        public bool ExchangeRate_Used { get; set; }

        // [DisplayName("Narration")]
        //public string ExchangeRate_Narration { get; set; }
    }

    public class ExchangeRateInfoModel
    {
        [Key]
        public long ExchangeRate_Id { get; set; }
        [DisplayName("Currency")]
        public long Currency_Id { get; set; }
        [DisplayName("No")]
        public byte No { get; set; }
        //[DisplayName("Currency Name")]
        //public string Currency_Name { get; set; }

        [DisplayName("Rate")]
        public decimal Rate { get; set; }
        [DisplayName("Date")]
        public string ExchangeRate_Date { get; set; }
        //public bool ExchangeRate_Used { get; set; }

        [DisplayName("Narration")]
        public string ExchangeRate_Narration { get; set; }
        public MessageViewModel OprMessage { get; set; }
        public ExchangeRateInfoModel()
        {
            OprMessage = new MessageViewModel();

        }
    }

    //public class ExchangeRateInfoModel 
    //{

    //    public ExchangeRateListModel ExchangeRateListModel { set; get; }
    //    public List<ExchangeCurrencyInfoModel> ExchangeCurrencyInfoModel { set; get; }

    //}

    //public class ExchangeCurrencyInfoModel
    //{
    //    [Key]
    //    public long Currency_Id { get; set; }
    //    public string Currency_Name { get; set; }
    //}

}
