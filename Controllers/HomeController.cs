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

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
