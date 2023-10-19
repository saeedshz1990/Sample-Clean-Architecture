using Sample_Clean_Architecture.Web.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Project
{
    public class ProjectModel
    {
        [Key]
        public int Projects_Id { get; set; }
        [DisplayName("Project Number")]
        public string Projects_Number { get; set; }
        [DisplayName("Project Name")]
        public string Projects_Name { get; set; }
        [DisplayName("Status")]
        public string Status_Description { get; set; }
        public bool Projects_Used { get; set; }
    }

    public class ProjectInfoModel : ProjectModel
    {
        public int Company_Id { get; set; }
        [DisplayName("Project Description")]
        public string Projects_Description { get; set; }
        [DisplayName("Project Start Date")]
        public string Projects_StartDate { get; set; }
        [DisplayName("Project End Date")]
        public string Projects_EndDate { get; set; }
        [DisplayName("Cost Center")]
        public int CostCenter_Id { get; set; }
        [DisplayName("Status")]
        public byte Projects_Status { get; set; }
        public object ExchangeRate_Date { get; internal set; }
        public List<int> Users_Project { get; set; }
        public MessageViewModel OprMessage { get; set; }
        public ProjectInfoModel()
        {
            Users_Project = new List<int>();
            OprMessage = new MessageViewModel();
        }

    }
}
