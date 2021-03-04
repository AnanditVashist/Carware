using Carware.Data;
using Carware.Interface;
using Carware.Models;
using Carware.StringConstants;
using Carware.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
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

        public async Task DeleteUserInDb(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var employeesSupervised = _dbContext.Users.Where(u => u.SupervisorId == id);
            foreach (var emplooyee in employeesSupervised)
            {
                emplooyee.SupervisorId = null;
            }
            _dbContext.SaveChanges();
            await _dbContext.SaveChangesAsync();
            await _userManager.DeleteAsync(user);
        }

        public async Task EditUserInDb(UserViewModel viewModel)
        {

            var userInDb = await _userManager.FindByIdAsync(viewModel.Id);
            var userInDbRole = await _userManager.GetRolesAsync(userInDb);

            userInDb.FirstName = viewModel.FirstName;
            userInDb.LastName = viewModel.LastName;
            userInDb.Email = viewModel.Email;
            userInDb.PhoneNumber = viewModel.PhoneNumber;
            userInDb.SupervisorId = viewModel.SupervisorId;
            userInDb.UserName = viewModel.Email;


            await _userManager.RemoveFromRoleAsync(userInDb, userInDbRole[0]);

            await _userManager.AddToRoleAsync(userInDb, viewModel.RoleName);

            await _dbContext.SaveChangesAsync();
        }

        public object GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public async Task<UserViewModel> GetCreateUserViewModelAsync()
        {
            var viewModel = new UserViewModel();
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

        public async Task<UserViewModel> GetEditUserViewModelAsync(string id)
        {
            var userInDb = await _userManager.FindByIdAsync(id);
            var viewModel = new UserViewModel
            {
                FirstName = userInDb.FirstName,
                LastName = userInDb.LastName,
                Email = userInDb.Email,
                PhoneNumber = userInDb.PhoneNumber,
                SupervisorId = userInDb.SupervisorId
            };
            foreach (var role in _roleManager.Roles)
            {
                var roleIndb = new SelectListItem
                {
                    Value = role.Name,
                    Text = role.Name,
                };
                if (await _userManager.IsInRoleAsync(userInDb, role.Name))
                {
                    roleIndb.Selected = true;
                }

                viewModel.Roles.Add(roleIndb);
            }
            var smList = await _userManager.GetUsersInRoleAsync(AuthorizationRoles.SalesManager);
            if (!(smList.Count == 0))
            {
                for (int i = 0; i < smList.Count; i++)
                {
                    if (smList[i].Id != id)
                    {
                        var smInDb = new SelectListItem
                        {
                            Value = smList[i].Id,
                            Text = (smList[i].FirstName + " " + smList[i].LastName),

                        };
                        if (smList[i].Id == userInDb.SupervisorId)
                        {
                            smInDb.Selected = true;
                        }
                        viewModel.SupervisorNames.Add(smInDb);

                    }
                }
            }
            var gmList = await _userManager.GetUsersInRoleAsync(AuthorizationRoles.GeneralManager);
            if (!(gmList.Count == 0))
            {
                for (int i = 0; i < gmList.Count; i++)
                {
                    if (gmList[i].Id != id)
                    {
                        var gmInDb = new SelectListItem
                        {
                            Value = gmList[i].Id,
                            Text = (gmList[i].FirstName + " " + gmList[i].LastName),

                        };
                        if (gmList[i].Id == userInDb.SupervisorId)
                        {
                            gmInDb.Selected = true;
                        }
                        viewModel.SupervisorNames.Add(gmInDb);
                    }
                }
            }




            return viewModel;
        }

        public async Task SaveUserInDb(UserViewModel viewModel)
        {
            var user = new ApplicationUser
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                UserName = viewModel.Email,
                SupervisorId = viewModel.SupervisorId

            };
            var pswd = "Password1!";
            var createUser = await _userManager.CreateAsync(user, pswd);
            if (!createUser.Succeeded)
            {
                var error = createUser.Errors;
            }

            await _userManager.AddToRoleAsync(user, viewModel.RoleName);
            await _dbContext.SaveChangesAsync();
        }
    }
}
