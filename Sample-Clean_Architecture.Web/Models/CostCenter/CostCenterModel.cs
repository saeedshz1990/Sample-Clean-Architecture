using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.CostCenter
{
    public class CostCenterModel
    {
        [Key]
        public int CostCenter_Id { get; set; }
        [DisplayName("Name")]
        public string CostCenter_Name { get; set; }
        [DisplayName("Status")]
        public string Status_Description { get; set; }
        public bool CostCenter_Used { get; set; }

    }

    public class CostCenterInfoModel : CostCenterModel
    {
        public byte CostCenter_Status { get; set; }

        [DisplayName("Description")]
        public string CostCenter_Description { get; set; }
        public MessageViewModel OprMessage { get; set; }
        public CostCenterInfoModel()
        {
            OprMessage = new MessageViewModel();

        }
    }
}
