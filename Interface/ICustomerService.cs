using Carware.Models;
using Carware.ViewModels;
using System.Collections.Generic;

namespace Carware.Interface
{
    public interface ICustomerService
    {
        void CreateAndSaveCustomerInDb(CustomerViewModel viewModel);
        void UpdateCustomerInDb(Customer viewModel);
        Customer FindCustomerById(int id);
        List<Customer> GetAllCustomers();

    }
}
