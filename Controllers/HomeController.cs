using JewelleryStoreManagementSystem.Data.Repositories;
using JewelleryStoreManagementSystem.Data.Services;
using Microsoft.AspNetCore.Mvc;
#nullable enable

namespace JewelleryStoreManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerRepository _customerRepository;
        private readonly ProductRepository _productRepository;
        private readonly OrderService _orderService;

        public HomeController(CustomerRepository customerRepository, ProductRepository productRepository, OrderService orderService)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderService = orderService;
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
