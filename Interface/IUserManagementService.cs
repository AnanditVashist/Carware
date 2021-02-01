using Carware.ViewModels;
using System.Threading.Tasks;

namespace Carware.Interface
{
    public interface IUserManagementService
    {
        Task<CreateUserViewModel> GetCreateUserViewModelAsync();
        Task SaveUserInDb(CreateUserViewModel viewModel);
    }
}
