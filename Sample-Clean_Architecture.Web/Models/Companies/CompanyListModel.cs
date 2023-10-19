using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Companies
{
    public class CompanyListModel
    {
        [Key]
        public int CompanyId { get; set; }
        [DisplayName("Bussiness Name")]
        public string BusinessName { get; set; }
        [DisplayName("Alias Name")]
        public string AliasName { get; set; }
        public byte TransactionType { get; set; }
    }
}
