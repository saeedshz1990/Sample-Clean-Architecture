using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Web.Models.Companies
{
    public class CompanyFinancialCycleModel
    {
        [Key]
        public int FinancialCycle_Id { get; set; }
        public int Company_Id { get; set; }
        [DisplayName("FinancialCycleTitle")]
        [Required(ErrorMessage = "{0} is required")]
        public string FinancialCycle_Title { get; set; }
        [DisplayName("FinancialCycleFromDate")]
        [Required(ErrorMessage = "{0} is required")]
        public string FinancialCycle_FromDate { get; set; }
        [DisplayName("FinancialCycleToDate")]
        public string FinancialCycle_ToDate { get; set; }
        [DisplayName("FinancialCycleisActive")]
        [Required(ErrorMessage = "{0} is required")]
        public bool FinancialCycle_isActive { get; set; }

    }
}
