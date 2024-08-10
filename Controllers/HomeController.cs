using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryStoreManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerRepository _customerRepository;

        public HomeController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IActionResult CustomerPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
