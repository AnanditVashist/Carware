using Carware.Data;
using Carware.Interface;
using Carware.Models;
using Carware.StringConstants;
using Carware.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Carware.Services
{
    public class UserManagementService : IUserManagementService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _dbContext;
        public UserManagementService(ApplicationDbContext dbContext, UserManager<ApplicationUser> UserManager, RoleManager<IdentityRole> RoleManager)
        {
            _dbContext = dbContext;
            _userManager = UserManager;
            _roleManager = RoleManager;
        }

        public async Task<CreateUserViewModel> GetCreateUserViewModelAsync()
        {
            var viewModel = new CreateUserViewModel();
            foreach (var role in _roleManager.Roles)
            {
                var roleIndb = new SelectListItem
                {
                    Value = role.Name,
                    Text = role.Name
                };
                viewModel.Roles.Add(roleIndb);
            }
            var smList = await _userManager.GetUsersInRoleAsync(AuthorizationRoles.SalesManager);
            if (!(smList.Count == 0))
            {
                for (int i = 0; i < smList.Count; i++)
                {
                    var smInDb = new SelectListItem
                    {
                        Value = smList[i].Id,
                        Text = (smList[i].FirstName + " " + smList[i].LastName),

                    };
                    viewModel.SupervisorNames.Add(smInDb);
                }
            }
            var gmList = await _userManager.GetUsersInRoleAsync(AuthorizationRoles.GeneralManager);
            if (!(gmList.Count == 0))
            {
                for (int i = 0; i < gmList.Count; i++)
                {
                    var gmInDb = new SelectListItem
                    {
                        Value = gmList[i].Id,
                        Text = (gmList[i].FirstName + " " + gmList[i].LastName),

                    };
                    viewModel.SupervisorNames.Add(gmInDb);
                }
            }




            return viewModel;
        }

        public async Task SaveUserInDb(CreateUserViewModel viewModel)
        {
            var user = new ApplicationUser
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.EmailAddress,
                PhoneNumber = viewModel.PhoneNumber,
                UserName = viewModel.EmailAddress

            };
            var pswd = "Password1!";
            var createUser = await _userManager.CreateAsync(user, pswd);
            if (!createUser.Succeeded)
            {
                var error = createUser.Errors;
            }

            await _userManager.AddToRoleAsync(user, viewModel.RoleName);
        }
    }
}
