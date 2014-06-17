using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.Account
{
    public class SignInViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Web.Resources.SignIn), ErrorMessageResourceName = "EMAIL_REQUIRED")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}