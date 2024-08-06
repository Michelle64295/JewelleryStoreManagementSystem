using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryStoreManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly AdminRepository _adminRepository;
        private readonly CustomerRepository _customerRepository;

        public LoginController(AdminRepository adminRepository, CustomerRepository customerRepository)
        {
            _adminRepository = adminRepository;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (_adminRepository.IsValidAdminCredentials(email, password))
            {
                return View("AdminPage");
            }
            if (_customerRepository.IsValidCustomerCredentials(email, password))
            {
                return RedirectToAction("Home", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Email or Password.");
                return View();
            }
        }

        [HttpPost]
        public IActionResult SignUp(string email, string password)
        {
            return RedirectToAction("Home", "Home");
        }

        public IActionResult AdminPage()
        {
            return View();
        }

        public IActionResult CustomerPage()
        {
            return View();
        }
    }
}
