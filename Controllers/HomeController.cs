using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryStoreManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdminRepository _adminRepository;
        private readonly CustomerRepository _customerRepository;

        public HomeController(AdminRepository adminRepository, CustomerRepository customerRepository)
        {
            _adminRepository = adminRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult AdminPage()
        {
            return View();
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
