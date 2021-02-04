using Carware.Data;
using Carware.Interface;
using Carware.Models;
using Carware.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;

namespace Carware.Services
{
    public class CarService : ICarService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _dbContext;
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        public CarService(ApplicationDbContext dbContext, UserManager<ApplicationUser> UserManager, RoleManager<IdentityRole> RoleManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = UserManager;
            _roleManager = RoleManager;
            _httpContextAccessor = httpContextAccessor;

        }

        public string GetCurrentLoggedInUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public void SaveCarInDb(CarViewModel viewModel)
        {
            var car = TurnViewModelToCar(viewModel);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
        }

        public CarViewModel TurnCarToViewModel(Car car)
        {
            var viewModel = new CarViewModel
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = (car.Year).ToString(),
                PurchaseDate = car.PurchaseDate,
                PurchasePrice = (car.PurchasePrice).ToString(),
                IdealSellingPrice = (car.IdealSellingPrice).ToString(),
                MaxDiscount = (car.MaxDiscount).ToString(),
            };
            return viewModel;
        }

        public Car TurnViewModelToCar(CarViewModel viewModel)
        {
            var car = new Car
            {
                Id = viewModel.Id,
                Make = viewModel.Make,
                Model = viewModel.Model,
                Year = Int32.Parse(viewModel.Year),
                PurchaseDate = viewModel.PurchaseDate,
                PurchasePrice = Int32.Parse(viewModel.PurchasePrice),
                IdealSellingPrice = Int32.Parse(viewModel.IdealSellingPrice),
                MaxDiscount = Int32.Parse(viewModel.MaxDiscount)
            };

            return car;
        }



        public void UpdateCarInDb(CarViewModel viewModel)
        {
            var carIndb = _dbContext.Cars.Find(viewModel.Id);
            carIndb.Make = viewModel.Make;
            carIndb.Model = viewModel.Model;
            carIndb.Year = Int32.Parse(viewModel.Year);
            carIndb.PurchaseDate = viewModel.PurchaseDate;
            carIndb.PurchasePrice = Int32.Parse(viewModel.PurchasePrice);
            carIndb.IdealSellingPrice = Int32.Parse(viewModel.IdealSellingPrice);
            carIndb.MaxDiscount = Int32.Parse(viewModel.MaxDiscount);

            _dbContext.SaveChanges();
        }

        public CarViewModel GetEditCarViewModel(int id)
        {
            var carInDb = _dbContext.Cars.Find(id);
            return TurnCarToViewModel(carInDb);

        }

        public InventoryViewModel GetInventory()
        {
            var inventory = new InventoryViewModel();

            var inventoryInDb = _dbContext.Cars.ToList();

            foreach (var car in inventoryInDb)
            {
                var viewModel = TurnCarToViewModel(car);
                inventory.Inventory.Add(viewModel);
            }

            return inventory;
        }
    }
}
