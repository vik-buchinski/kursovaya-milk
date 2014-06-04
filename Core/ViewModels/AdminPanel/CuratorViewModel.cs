using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.AdminPanel
{
    public class CuratorViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Patronymic is required.")]
        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Work place is required.")]
        [Display(Name = "Work place")]
        public string WorkPlace { get; set; }

        [Required(ErrorMessage = "Chair is required.")]
        [Display(Name = "Chair(кафедра)")]
        public string Chair { get; set; }

        [Required(ErrorMessage = "Post is required.")]
        [Display(Name = "Post")]
        public string Post { get; set; }

        [Required(ErrorMessage = "Academic title is required.")]
        [Display(Name = "Academic title")]
        public string AcademicTitle { get; set; }

        [Required(ErrorMessage = "Degree is required.")]
        [Display(Name = "Degree")]
        public string Degree { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}