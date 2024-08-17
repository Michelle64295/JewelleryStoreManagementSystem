using Microsoft.AspNetCore.Mvc;
using JewelleryStoreManagementSystem.Data.Services;
using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;

namespace JewelleryStoreManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly ProductRepository _productRepository;


        public OrderController(OrderService orderService, OrderItemRepository orderItemRepository, ProductRepository productRepository)
        {
            _orderService = orderService;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult Order(int quantity, int productId)
        {
            int customerId = (int)TempData["CustomerId"];
            Order order = _orderService.GetOrder(quantity, productId, customerId);
            TempData.Keep("CustomerId");
            return View(order);
        }

        [HttpGet]
        public IActionResult Order()
        {
            int customerId = (int)TempData["CustomerId"];
            Order order = _orderService.GetOrder(customerId);
            TempData.Keep("CustomerId");
            return View(order);
        }

        [HttpPost]
        public IActionResult CompleteOrder()
        {
            int customerId = (int)TempData["CustomerId"];
            _orderService.DeleteOrderItems(customerId);
            Order order = _orderService.GetOrder(customerId);
            TempData.Keep("CustomerId");
            return RedirectToAction("Order", order);
        }

        [HttpPost]
        public IActionResult Remove(string name)
        {
            int customerId = (int)TempData["CustomerId"];
            Order order = _orderService.GetOrder(customerId);
            TempData.Keep("CustomerId");

            Product product = _productRepository.GetProductByName(name);
            OrderItem orderItem = _orderItemRepository.GetOrderItemByOrderIdAndProductId(order.OrderId, product.ProductId);

            _orderService.DeleteOrderItem(orderItem);
            return RedirectToAction("Order", order);
        }
    }
}
