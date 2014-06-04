using DataAcess.Models.Conference;

namespace Core.ViewModels.Conference
{
    public class TakePartCategotyViewModel
    {
        public ConferenceCategoryModel Category { get; set; }
        public int RegisteredCount { get; set; }
    }
}