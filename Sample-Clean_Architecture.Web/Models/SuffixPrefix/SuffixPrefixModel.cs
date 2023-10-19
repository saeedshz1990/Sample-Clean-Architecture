using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.SuffixPrefix
{
    public class SuffixPrefixModel
    {
        [Key]
        public int SuffixPrefix_Id { get; set; }
        public int Company_Id { get; set; }
        public int VoucherType_Id { get; set; }
        [DisplayName("Voucher Type")]
        public string voucherType_Name { get; set; }
        [DisplayName("No")]
        public string No { get; set; }
        [DisplayName("FromDate")]
        [Required(ErrorMessage = "{0} is required")]
        public string SuffixPrefix_FromDate { get; set; }
        [DisplayName("ToDate")]
        public string SuffixPrefix_ToDate { get; set; }
        [DisplayName("StartIndex")]
        [Required(ErrorMessage = "{0} is required")]
        public int SuffixPrefix_StartIndex { get; set; }
        [DisplayName("Prefix")]
        public string SuffixPrefix_Prefix { get; set; }
        [DisplayName("Suffix")]
        public string SuffixPrefix_Suffix { get; set; }
        [DisplayName("Width Of Numerical Part")]
        public byte SuffixPrefix_widthOfNumericalPart { get; set; }
        [DisplayName("Prefill With Character")]
        public string SuffixPrefix_PrefillWithCharacter { get; set; }
        [DisplayName("Status")]
        public byte SuffixPrefix_Status { get; set; }
        [DisplayName("Narration")]
        public string Narration { get; set; }
        public MessageViewModel OprMessage { get; set; }
        public SuffixPrefixModel()
        {
            OprMessage = new MessageViewModel();

        }
    }
}
