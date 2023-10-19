using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Web.Models.Companies
{
    public class CompanyBranchModel
    {
        [Key]
        public int CompanyBranch_Id { get; set; }
        public int Company_Id { get; set; }
        [DisplayName("BranchTitle")]
        [Required(ErrorMessage = "{0} is required")]
        public string CompanyBranch_Title { get; set; }

    }
}
