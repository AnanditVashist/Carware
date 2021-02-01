using System.Collections.Generic;

namespace Carware.Models
{
    public class Supervisor : ApplicationUser
    {
        public ICollection<ApplicationUser> EmployeesSupervised { get; set; }
    }
}
