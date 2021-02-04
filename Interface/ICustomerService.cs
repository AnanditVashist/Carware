using Carware.Models;
using Carware.ViewModels;

namespace Carware.Interface
{
    public interface ICustomerService
    {
        void CreateAndSaveCustomerInDb(CustomerViewModel viewModel);
        void UpdateCustomerInDb(Customer viewModel);
        Customer FindCustomerById(int id);
    }
}
