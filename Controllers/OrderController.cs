using Microsoft.AspNetCore.Mvc;
using JewelleryStoreManagementSystem.Data.Services;
using JewelleryStoreManagementSystem.Data.Models;

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
        public IActionResult Order(int quantity, int productId)
        {
            //int customerId = Convert.ToInt32(TempData["CustomerId"]);
            int customerId = (int)TempData["CustomerId"];
            var order = _orderService.GetOrder(quantity, productId, customerId);
            TempData.Keep("CustomerId");
            return View(order);
        }

        [HttpGet]
        public IActionResult Order()
        {
            //int customerId = Convert.ToInt32(TempData["CustomerId"]);
            int customerId = (int)TempData["CustomerId"];
            Order order = _orderService.GetOrder(customerId);
            TempData.Keep("CustomerId");
            return View(order);
        }
    }
}
