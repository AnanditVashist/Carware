using Carware.Interface;
using Carware.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Sell()
        {
            return View();
        }

        public IActionResult Inventory()
        {

            return View(_carService.GetInventory());
        }

    }
}
