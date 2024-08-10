using JewelleryStoreManagementSystem.Data.Repositories;
using JewelleryStoreManagementSystem.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Xml.Linq;

namespace JewelleryStoreManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerService _customerService;

        public LoginController(CustomerRepository customerRepository, CustomerService customerService)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
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

        [HttpGet]
        public IActionResult SignUp()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult SignUp(string name, string email, int streetNumber, string street, string city, string state, string phoneNumber, string password)
        {
            if (!ValidationService.IsValidFullName(name))
            {
                ModelState.AddModelError(string.Empty, "Invalid Name. Full Name must consist of only letters and spaces!");
                return View("Login");
            }
            if (!ValidationService.IsValidEmail(email) || _customerRepository.GetAllCustomersByEmail().Contains(email))
            {
                ModelState.AddModelError(string.Empty, "Invalid Email!");
                return View("Login");
            }
            if (!ValidationService.IsValidString(street))
            {
                ModelState.AddModelError(string.Empty, "Invalid Street Name. Street Name must consist of only letters and spaces!");
                return View("Login");
            }
            if (!ValidationService.IsValidString(city))
            {
                ModelState.AddModelError(string.Empty, "Invalid City. City must consist of only letters and spaces!");
                return View("Login");
            }
            if (!ValidationService.IsValidPhone(phoneNumber))
            {
                ModelState.AddModelError(string.Empty, "Invalid Phone Number. Phone Number must be 10 digits long and start with '04'!");
                return View("Login");
            }
            if (!ValidationService.IsValidPassword(password))
            {
                ModelState.AddModelError(string.Empty, "Invalid Password. Password must be at least 5 characters long!");
                return View("Login");
            }
            else
            {
                _customerService.AddCustomer(name, email, streetNumber, street, city, state, phoneNumber, password);
                return RedirectToAction("Home", "Home");
            }
        }

        public IActionResult CustomerPage()
        {
            return View();
        }
    }
}
