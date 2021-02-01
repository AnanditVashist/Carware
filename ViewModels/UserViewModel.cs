using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Carware.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; }
        public IList<SelectListItem> Roles { get; set; }
        public IList<SelectListItem> SupervisorNames { get; set; }

        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public string SupervisorId { get; set; }
        public UserViewModel()
        {
            Roles = new List<SelectListItem>();
            SupervisorNames = new List<SelectListItem>();
        }

    }

}
