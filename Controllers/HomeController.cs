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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(int id, string password)
        {
            List<(int adminId, string password)> adminCredentials = _adminRepository.GetAdminCredentials();
            List<(int customerId, string password)> customerCredentials = _customerRepository.GetCustomerCredentials();

            var admin = adminCredentials.FirstOrDefault(a => a.adminId == id && a.password == password);
            var customer = customerCredentials.FirstOrDefault(c => c.customerId == id && c.password == password);

            if (admin != default)
            {
                ViewBag.FullName = _adminRepository.GetAdminById(id).FullName;
                return View("AdminPage");
            }
            if (customer != default)
            {
                ViewBag.FullName = _customerRepository.GetCustomerById(id).FullName;
                return View("CustomerPage");
            }
            else
            {
                ViewBag.Message = "Invalid ID or Password";
                return View();
            }

        }

        public IActionResult AdminPage()
        {
            return View();
        }

        public IActionResult CustomerPage()
        {
            return View();
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
