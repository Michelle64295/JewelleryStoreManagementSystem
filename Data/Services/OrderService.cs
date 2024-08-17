using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
#nullable enable

namespace JewelleryStoreManagementSystem.Data.Services
{
    public class OrderService
    {
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly CustomerRepository _customerRepository;

        public OrderService(ProductRepository productRepository, OrderItemRepository orderItemRepository, OrderRepository orderRepository, CustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _customerRepository = customerRepository;
        }

        public void AddOrderItem(int quantity, int orderId, int productId)
        {
            OrderItem existingOrderItem = _orderItemRepository.GetOrderItemByOrderIdAndProductId(orderId, productId);

            if (existingOrderItem != null)
            {
                existingOrderItem.Quantity += quantity;
                _orderItemRepository.Update(existingOrderItem);
            }
            else
            {
                Product product = _productRepository.GetProductById(productId);

                OrderItem orderItem = new OrderItem()
                {
                    Quantity = quantity,
                    OrderId = orderId,
                    ProductId = productId,
                    Product = product
                };

                _orderItemRepository.Add(orderItem);
            }

            _orderItemRepository.SaveChanges();
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

                AddOrderItem(quantity, order.OrderId, productId);
                return order;
            }
            else
            {
                Order existingOrder = _orderRepository.GetOrderByCustomerId(customer.CustomerId);
                AddOrderItem(quantity, existingOrder.OrderId, productId);
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

        public void DeleteOrderItems(int customerId) 
        {
            Customer customer = _customerRepository.GetCustomerById(customerId);
            if (customer.Orders != null && customer.Orders.Count > 0)
            {
                List<OrderItem> orderItems = _orderItemRepository.GetAllOrderItemsInOrder(customer.Orders.First().OrderId);
                foreach (OrderItem item in orderItems) 
                {
                    _orderItemRepository.Remove(item);
                    _orderItemRepository.SaveChanges();
                }
            }
        }

        public void DeleteOrderItem(OrderItem orderItem)
        {
            _orderItemRepository.Remove(orderItem);
            _orderItemRepository.SaveChanges();
        }
    }
}
