using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Companies
{
    public class CurrentDateModel
    {
        [Display(Name = "Current Date")]
        [Required(ErrorMessage = "{0} is required")]
        public string CurrentDate { get; set; }
    }
}
