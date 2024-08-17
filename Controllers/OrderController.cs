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

        [HttpPost]
        public IActionResult ViewOrder(int quantity, int productId)
        {
            //int customerId = Convert.ToInt32(TempData["CustomerId"]);
            int customerId = (int)TempData["CustomerId"];
            var order = _orderService.GetOrder(customerId);
            TempData.Keep("CustomerId");
            return View(order);
        }

        //[HttpPost]
        //public IActionResult Order(int productId, int quantity)
        //{
        //    int customerId = Convert.ToInt32(TempData["CustomerId"]);
        //    _orderService.AddToOrder(productId, quantity, customerId);
        //    TempData.Keep("CustomerId");
        //    return RedirectToAction("Order");
        //}

        //[HttpGet]
        //public IActionResult Order()
        //{
        //    int customerId = Convert.ToInt32(TempData["CustomerId"]);
        //    var order = _orderService.GetOrder(customerId);
        //    return View(order);
        //}

        //[HttpPost]
        //public IActionResult CompleteOrder()
        //{
        //    _orderService.CompleteOrder();
        //    return RedirectToAction("Order");
        //}
    }
}
