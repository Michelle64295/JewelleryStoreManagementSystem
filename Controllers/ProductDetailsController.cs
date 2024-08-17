using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
#nullable enable

namespace JewelleryStoreManagementSystem.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly ProductRepository _productRepository;
        public ProductDetailsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult ProductDetails(int id)
        {
            var product = _productRepository.GetProductById(id);
            return View(product);
        }
    }
}
