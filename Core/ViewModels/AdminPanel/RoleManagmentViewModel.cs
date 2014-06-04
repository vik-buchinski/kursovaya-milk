using System.Collections.Generic;

namespace Core.ViewModels.AdminPanel
{
    public class RoleManagmentViewModel
    {
        public IEnumerable<string> available_roles { get; set; }

        public IEnumerable<string> current_roles { get; set; } 
    }
}