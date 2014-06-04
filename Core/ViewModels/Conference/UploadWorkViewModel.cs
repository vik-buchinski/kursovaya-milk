using System.Collections.Generic;
using DataAcess.Models.ConferenceMember;

namespace Core.ViewModels.Conference
{
    public class UploadWorkViewModel
    {
        public int MemberId { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<MemberWorkModel> UploadedWorks { get; set; }
    }
}