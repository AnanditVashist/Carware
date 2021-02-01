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
            return View();
        }
        public async Task<IActionResult> Create()
        {


            var viewModel = await _userManagementService.GetCreateUserViewModelAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel user)
        {
            await _userManagementService.SaveUserInDb(user);
            return RedirectToAction("Index");
        }

    }
}
