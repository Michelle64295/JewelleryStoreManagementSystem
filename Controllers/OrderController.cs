using JewelleryStoreManagementSystem.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryStoreManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
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
