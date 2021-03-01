using Carware.Interface;
using Carware.Models;
using Carware.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Carware.Controllers
{
    public class CustomerController : Controller
    {
        public ICustomerService _customerService { get; set; }

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;

        }
        public IActionResult Index()
        {
            return View(_customerService.GetAllCustomers());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel viewModel)
        {
            _customerService.CreateAndSaveCustomerInDb(viewModel);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_customerService.FindCustomerById(id));
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _customerService.UpdateCustomerInDb(customer);
            return RedirectToAction("Index");
        }



    }
}
