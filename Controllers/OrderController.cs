using Microsoft.AspNetCore.Mvc;
using JewelleryStoreManagementSystem.Data.Services;
using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
using Castle.Core.Resource;
#nullable enable

namespace JewelleryStoreManagementSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly OrderItemService _orderItemService;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly ProductRepository _productRepository;


        public OrderController(OrderService orderService, OrderItemRepository orderItemRepository, ProductRepository productRepository, OrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public IActionResult Order(int quantity, int productId)
        {
            int? customerId = (int?)TempData["CustomerId"];
            if (customerId != null)
            {
                int id = (int)customerId;
                Order order = _orderService.GetOrder(quantity, productId, id);
                TempData.Keep("CustomerId");
                return View(order);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        [HttpGet]
        public IActionResult Order()
        {
            int? customerId = (int?)TempData["CustomerId"];
            if (customerId != null)
            {
                int id = (int)customerId;
                Order order = _orderService.GetOrder(id);
                TempData.Keep("CustomerId");
                return View(order);
            }
            else
            {
                throw new NullReferenceException(); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder()
        {
            int? customerId = (int?)TempData["CustomerId"];
            if (customerId != null)
            {
                int id = (int)customerId;
                await _orderService.CompleteOrderAsync(id);
                _orderItemService.DeleteOrderItems(id);
                Order order = _orderService.GetOrder(id);
                TempData.Keep("CustomerId");
                return RedirectToAction("Order", order);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        [HttpPost]
        public IActionResult Remove(string name)
        {
            int? customerId = (int?)TempData["CustomerId"];
            if (customerId != null)
            {
                int id = (int)customerId;
                Order order = _orderService.GetOrder(id);
                TempData.Keep("CustomerId");

                Product product = _productRepository.GetProductByName(name);
                OrderItem? orderItem = _orderItemRepository.GetOrderItemByOrderIdAndProductId(order.OrderId, product.ProductId);

                if (orderItem != null) 
                {
                    _orderItemService.DeleteOrderItem(orderItem);
                    return RedirectToAction("Order", order);
                }
                else
                {
                    throw new NullReferenceException(); 
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
