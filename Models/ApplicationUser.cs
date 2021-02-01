using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Carware.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Car> CarsSold { get; set; }
        public Supervisor Supervisor { get; set; }
        public string SupervisorId { get; set; }

    }
}
