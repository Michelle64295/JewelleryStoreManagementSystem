using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
#nullable enable

namespace JewelleryStoreManagementSystem.Data.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly OrderItemService _orderItemService;

        public OrderService(OrderRepository orderRepository, CustomerRepository customerRepository, OrderItemService orderItemService)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderItemService = orderItemService;
        }

        public Order GetOrder(int quantity, int productId, int customerId)
        {
            Customer customer = _customerRepository.GetCustomerById(customerId);
            if (customer.Orders != null && customer.Orders.Count == 0) 
            {
                Order order = new Order
                {
                    CustomerId = customer.CustomerId,
                    OrderDate = DateTime.Now
                };

                _orderRepository.Add(order);
                _orderRepository.SaveChanges();

                _orderItemService.AddOrderItem(quantity, order.OrderId, productId);
                return order;
            }
            else
            {
                Order existingOrder = _orderRepository.GetOrderByCustomerId(customer.CustomerId);
                _orderItemService.AddOrderItem(quantity, existingOrder.OrderId, productId);
                return existingOrder;
            }
        }

        public Order GetOrder(int customerId)
        {
            Customer customer = _customerRepository.GetCustomerById(customerId);
            if (customer.Orders != null && customer.Orders.Count == 0)
            {
                Order order = new Order
                {
                    CustomerId = customer.CustomerId,
                    OrderDate = DateTime.Now
                };

                _orderRepository.Add(order);
                _orderRepository.SaveChanges();
                return order;
            }
            else
            {
                Order existingOrder = _orderRepository.GetOrderByCustomerId(customer.CustomerId);
                return existingOrder;
            }
        }
    }
}
