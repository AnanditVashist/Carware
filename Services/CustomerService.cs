using Carware.Data;
using Carware.Interface;
using Carware.Models;
using Carware.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Carware.Services
{
    public class CustomerService : ICustomerService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _dbContext;
        public CustomerService(ApplicationDbContext dbContext, UserManager<ApplicationUser> UserManager, RoleManager<IdentityRole> RoleManager)
        {
            _dbContext = dbContext;
            _userManager = UserManager;
            _roleManager = RoleManager;
        }
        public void CreateAndSaveCustomerInDb(CustomerViewModel viewModel)
        {
            var customer = new Customer
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                EmailAddress = viewModel.EmailAddress,
                PhoneNumber = viewModel.PhoneNumber
            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

        }

        public Customer FindCustomerById(int id)
        {
            return _dbContext.Customers.Find(id);
        }

        public void UpdateCustomerInDb(Customer viewModel)
        {
            var customerInDb = _dbContext.Customers.Find(viewModel.Id);
            customerInDb.FirstName = viewModel.FirstName;
            customerInDb.LastName = viewModel.LastName;
            customerInDb.EmailAddress = viewModel.EmailAddress;
            customerInDb.PhoneNumber = viewModel.PhoneNumber;

            _dbContext.SaveChanges();
        }
    }
}
