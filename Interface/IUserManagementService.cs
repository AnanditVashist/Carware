using Carware.ViewModels;
using System.Threading.Tasks;

namespace Carware.Interface
{
    public interface IUserManagementService
    {
        Task<UserViewModel> GetCreateUserViewModelAsync();
        Task<UserViewModel> GetEditUserViewModelAsync(string id);
        Task SaveUserInDb(UserViewModel viewModel);
        Task EditUserInDb(UserViewModel viewModel);
    }
}
