using Carware.Data;
using Carware.Interface;
using Carware.Models;
using Carware.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            var photoList = new List<Photo>();
            foreach (var photoInViewModel in viewModel.Photos)
            {
                var photo = new Photo();
                using (var ms = new MemoryStream())
                {
                    photoInViewModel.
                        CopyTo(ms);
                    photo.PhotoBlob = ms.ToArray();
                }
                photo.CarId = viewModel.Id;
                photoList.Add(photo);
            }
            car.Photos = photoList;

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
        }

        public async Task<CarViewModel> TurnCarToViewModelAsync(Car car)
        {
            var viewModel = new CarViewModel
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = (car.Year).ToString(),
                PurchaseDate = car.PurchaseDate,
                PurchasePrice = (car.PurchasePrice).ToString(),
                IdealSellingPrice = car.IdealSellingPrice.ToString(),
                MaxDiscount = (car.MaxDiscount).ToString(),
                Mileage = car.Mileage

            };

            foreach (var photo in car.Photos)
            {
                var image64String = Convert.ToBase64String(photo.PhotoBlob);
                viewModel.PhotoString.Add(image64String);
            }
            if (car.SellerId != null)
            {
                viewModel.SellingPrice = car.ActualSellingPrice.ToString();
                viewModel.SellDate = (DateTime)car.SellingDate;
                viewModel.Seller = await _userManager.FindByIdAsync(car.SellerId);

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
                MaxDiscount = Int32.Parse(viewModel.MaxDiscount),
                Mileage = viewModel.Mileage
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
            carIndb.Mileage = viewModel.Mileage;
            _dbContext.SaveChanges();
        }

        public Task<CarViewModel> GetEditCarViewModel(int id)
        {
            var carInDb = _dbContext.Cars.Find(id);
            return TurnCarToViewModelAsync(carInDb);
        }

        public async Task<InventoryViewModel> GetInventory()
        {
            var inventory = new InventoryViewModel();

            var inventoryInDb = _dbContext.Cars.Include(c => c.Photos).ToList();

            foreach (var car in inventoryInDb)
            {
                var viewModel = await TurnCarToViewModelAsync(car);
                inventory.Inventory.Add(viewModel);

            }

            return inventory;
        }

        public async Task<SellCarViewModel> GetSellCarViewModel(int carId)
        {
            var viewModel = new SellCarViewModel();
            viewModel.Car = await TurnCarToViewModelAsync(_dbContext.Cars.Include(c => c.Photos).First(c => c.Id == carId));

            return viewModel;
        }

        public void SellCar(SellCarViewModel viewModel)
        {
            var carInDb = _dbContext.Cars.Find(viewModel.Car.Id);
            carInDb.ActualSellingPrice = Convert.ToInt32(viewModel.SellingPrice);
            carInDb.SellingDate = DateTime.Today;
            carInDb.SellerId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var customer = new Customer
            {
                FirstName = viewModel.CustomerDetails.FirstName,
                LastName = viewModel.CustomerDetails.LastName,
                EmailAddress = viewModel.CustomerDetails.EmailAddress,
                PhoneNumber = viewModel.CustomerDetails.PhoneNumber,
                Car = carInDb
            };

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }
        public async Task<InventoryViewModel> SoldVehicles()
        {
            var soldCars = new InventoryViewModel();

            var soldCarsInDb = _dbContext.Cars.Include(c => c.Photos).Where(c => c.SellingDate != null).ToList();

            foreach (var car in soldCarsInDb)
            {
                var viewModel = TurnCarToViewModelAsync(car);
                soldCars.Inventory.Add(await viewModel);
            }

            return soldCars;
        }


    }
}
