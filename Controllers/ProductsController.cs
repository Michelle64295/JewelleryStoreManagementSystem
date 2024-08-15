using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryStoreManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(CustomerRepository customerRepository, ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Products()
        {
            var products = _productRepository.GetAllProducts();
            return View(products);
        }
    }
}
