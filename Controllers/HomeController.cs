using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryStoreManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerRepository _customerRepository;
        private readonly ProductRepository _productRepository;

        public HomeController(CustomerRepository customerRepository, ProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
