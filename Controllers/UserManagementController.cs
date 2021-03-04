using Carware.Interface;
using Carware.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carware.Controllers
{
    public class UserManagementController : Controller
    {

        private IUserManagementService _userManagementService;
        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        public IActionResult Index()
        {
            return View(_userManagementService.GetAllUsers());
        }
        public async Task<IActionResult> Create()
        {


            var viewModel = await _userManagementService.GetCreateUserViewModelAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            await _userManagementService.SaveUserInDb(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var employeeInDb = await _userManagementService.GetEditUserViewModelAsync(id);
            return View(employeeInDb);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel viewModel)
        {
            await _userManagementService.EditUserInDb(viewModel);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _userManagementService.DeleteUserInDb(id);
            return RedirectToAction("Index");
        }



    }
}
