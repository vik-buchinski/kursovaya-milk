using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.Conference
{
    public class ConferenceCategoryViewModel
    {
        [Required(ErrorMessage = "Ctagory name is required.")]
        [Display(Name = "Type category name")]
        public string CategoryName { get; set; }

        public List<string> Categories { get; set; }
    }
}