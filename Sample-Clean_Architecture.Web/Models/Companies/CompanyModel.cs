using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample_Clean_Architecture.Web.Models.Companies
{
    public class CompanyModel
    {
        [Key]
        public int CompanyId { get; set; }
        public int Accounts_Id { get; set; }
        [DisplayName("Country")]
        public byte Country_Id { get; set; }


        [DisplayName("DateFormat")]
        public byte DateFormats_Id { get; set; }
        [Display(Name = "BussinessName")]
        [MaxLength(50, ErrorMessage = "Maximum 12 characters only")]
        [Required(ErrorMessage = "{0} is required")]
        public string BussinessName { get; set; }
        [Display(Name = "AliasName")]
        [Required(ErrorMessage = "{0} is required")]
        public string AliasName { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("PhoneNo")]
        public string PhoneNo { get; set; }
        [DisplayName("Fax")]
        public string Fax { get; set; }

        [StringLength(50, ErrorMessage = "Max {0} characters")]
        // [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [MaxLength(12)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} must be numeric")]
        [DisplayName("Mobile")]
        public string Mobile { get; set; }
        [DisplayName("Web")]
        public string Web { get; set; }
        [DisplayName("Date Seperator")]
        public byte DateSeperator { get; set; }
        //[DisplayName("Time Zone")]
        //public byte TimeZone { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [DisplayName("Until")]
        public byte TransactionType { get; set; }
        [DisplayName("Transaction Start")]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        //[DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string TransactionStart { get; set; }
        //[DisplayName("Company Logo")]
        //public byte[] CompanyLogo { get; set; }
        [NotMapped]
        [DisplayName("Company Logo")]
        public IFormFile CompanyLogo { get; set; }
        public string CompanyLogoBase64 { get; set; }
        [DisplayName("Tax1")]
        public string Tax1 { get; set; }
        [DisplayName("Tax2")]
        public string Tax2 { get; set; }
        [DisplayName("Tax3")]
        public string Tax3 { get; set; }

        [DisplayName("Sub Currency")]
        public string SubCurrency { get; set; }
        [DisplayName("Currency Name")]
        public string CurrencyName { get; set; }
        [DisplayName("Currency Name")]
        public byte Country_Currency_Id { get; set; }
        public bool Company_LedgerInserted { get; set; }
        public int DefaultLedgerId { get; set; }
        [DisplayName("Currency Autoupdate")]
        public bool CurrencyAutoupdate { get; set; }
        public MessageViewModel OprMessage { get; set; }
        public CompanyModel()
        {
            OprMessage = new MessageViewModel();
            TransactionStart = DateTime.Now.ToShortDateString();
        }
    }
}
