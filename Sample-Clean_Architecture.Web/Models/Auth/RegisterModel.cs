using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Auth
{
    public class RegisterModel
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter your {0}")]
        [EmailAddress(ErrorMessage = "The email must be a valid {0}.")]
        public string Email { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter your {0}")]
        [StringLength(30, ErrorMessage = "The {0} cannot be longer than {1} characters and less than {2} characters", MinimumLength = 3)]
        public string Name { get; set; }
        public string Password { get; set; }
    }
}