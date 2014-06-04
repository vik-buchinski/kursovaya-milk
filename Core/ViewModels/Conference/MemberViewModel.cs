using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.Conference
{
    public class MemberViewModel
    {
        public int CategoryId { get; set; }
        public int MemberId { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Town is required.")]
        [Display(Name = "Town")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Educational institution type is required.")]
        [Display(Name = "Educational institution type")]
        public string EducationalInstitution { get; set; }

        [Required(ErrorMessage = "Name or number is required.")]
        [Display(Name = "Name or number of educational institution")]
        public string NameOrNumber { get; set; }

        [Required(ErrorMessage = "Class or group is required.")]
        [Display(Name = "Class or group")]
        public string ClassOrGroup { get; set; }
    }
}