using JewelleryStoreManagementSystem.Data.Repositories;
using JewelleryStoreManagementSystem.Data.Services;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult CustomerPage()
        {
            return View();
        }

        public IActionResult Products()
        {
            var products = _productRepository.GetAllProducts();
            return View(products);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productRepository.GetProductById(id);
            return View(product);
        }

        public IActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Order(int productId, int quantity)
        {
            int customerId = Convert.ToInt32(TempData["CustomerId"]);
            _orderService.AddToOrder(productId, quantity, customerId);
            TempData.Keep("CustomerId");
            return RedirectToAction("Order");
        }

        [HttpGet]
        public IActionResult Order()
        {
            var order = _orderService.GetOrder();  
            return View(order);
        }
    }
}
