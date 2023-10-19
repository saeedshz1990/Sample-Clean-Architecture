using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Benefeciary
{
    public class BenefeciaryModel
    {
        [Key]
        public int Beneficiary_Id { get; set; }
        public int Company_Id { get; set; }
        [DisplayName("Beneficiary Name")]
        public string Beneficiary_Name { get; set; }

        [DisplayName("Mobile")]
        [RegularExpression("([0-9]+)")]
        public string Beneficiary_Mobile { get; set; }
        [DisplayName("Passport No.")]
        [RegularExpression("([0-9]*)", ErrorMessage = "Please enter valid Number")]
        public string Beneficiary_Passport { get; set; }
        [DisplayName("ID Number")]
        public string Beneficiary_IdNumber { get; set; }
        public BenefeciaryModel()
        {
            Beneficiary_Name = "";
            Beneficiary_Mobile = "";
            Beneficiary_Passport = "";
            Beneficiary_IdNumber = "";
        }
    }

    public class BenefeciaryInfoModel : BenefeciaryModel
    {
        [DisplayName("Ref No.")]
        public string Beneficiary_RefNo { get; set; }
        [DisplayName("Remark")]
        public string Beneficiary_Remark { get; set; }
        public int FinancialCycle_Id { get; internal set; }
        public BenefeciaryInfoModel()
        {
            Beneficiary_RefNo = "";
            Beneficiary_Remark = "";
        }
    }

}
