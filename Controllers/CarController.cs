﻿using Carware.Interface;
using Carware.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carware.Controllers
{
    public class CarController : Controller
    {
        public ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var viewModel = new CarViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(CarViewModel viewModel)
        {
            _carService.SaveCarInDb(viewModel);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {

            return View(_carService.GetEditCarViewModel(id));
        }

        [HttpPost]
        public IActionResult Edit(CarViewModel viewModel)
        {
            _carService.UpdateCarInDb(viewModel);
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Inventory()
        {

            return View(await _carService.GetInventory());
        }
        public async Task<IActionResult> Sell(int id)
        {
            return View(await _carService.GetSellCarViewModel(id));
        }

        [HttpPost]
        public IActionResult Sell(SellCarViewModel viewModel)
        {
            _carService.SellCar(viewModel);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SoldVehicles()
        {
            return View(await _carService.SoldVehicles());
        }

    }
}
