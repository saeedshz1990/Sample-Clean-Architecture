using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Auth
{
    public class ForgotModel
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter your {0}")]
        [EmailAddress(ErrorMessage = "The email must be a valid {0}.")]
        public string Email { get; set; }
    }
}
