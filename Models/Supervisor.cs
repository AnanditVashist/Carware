using System.Collections.Generic;

namespace Carware.Models
{
    public class Supervisor : ApplicationUser
    {
        public List<ApplicationUser> EmployeesSupervised { get; set; }
    }
}
