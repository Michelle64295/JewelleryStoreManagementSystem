using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JewelleryStoreManagementSystem.Data.Services
{
    public class OrderService
    {
        //private Order? _currentOrder;
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
            //_currentOrder = null;
        }

        //public void AddToOrder(int productId, int quantity, int customerId)
        //{
        //    // Get the current order or create a new one if none exists
        //    Order currentOrder = _orderRepository.GetOrdersByCustomerId(customerId)
        //                                  .LastOrDefault() ?? new Order
        //                                  {
        //                                      OrderDate = DateTime.Now,
        //                                      CustomerId = customerId
        //                                  };

        //    // If no existing order was found, add the new order to the repository
        //    if (currentOrder.OrderId == 0)
        //    {
        //        _orderRepository.AddOrder(currentOrder);
        //    }

        //    Product? product = _productRepository.GetProductById(productId);
        //    if (product == null)
        //    {
        //        throw new Exception("Product not found");
        //    }

        //    OrderItem? existingOrderItem = currentOrder.OrderItems.FirstOrDefault(x => x.ProductId == productId);

        //    if (existingOrderItem != null)
        //    {
        //        existingOrderItem.Quantity += quantity;
        //    }
        //    else
        //    {
        //        OrderItem orderItem = new OrderItem
        //        {
        //            ProductId = product.ProductId,
        //            Quantity = quantity,
        //            OrderId = currentOrder.OrderId
        //        };

        //        // Ensure you don't add the same item multiple times
        //        if (currentOrder.OrderItems.All(oi => oi.ProductId != productId))
        //        {
        //            currentOrder.OrderItems.Add(orderItem);
        //        }
        //    }

        //    _orderRepository.UpdateOrder(currentOrder);
        //}


        //public Order GetOrder(int customerId)
        //{
        //    var orders = _orderRepository.GetOrdersByCustomerId(customerId);
        //    return orders.LastOrDefault(); // Return the latest order
        //}

        //public Order? GetOrderById(int orderId)
        //{
        //    return _orderRepository.GetOrderById(orderId);
        //}

        //public void SetCustomer(Customer customer)
        //{
        //    if (_currentOrder != null)
        //    {
        //        _currentOrder.CustomerId = customer.CustomerId;
        //        _currentOrder.Customer = customer;
        //        _orderRepository.UpdateOrder(_currentOrder);
        //    }
        //}

        //public void CompleteOrder()
        //{
        //    _currentOrder = null; // Reset the current order after completion
        //}

        public void AddOrderItem(int quantity, int orderId, int productId)
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
            _orderItemRepository.SaveChanges();
        }

        public Order GetOrder(int quantity, int productId, int customerId)
        {
            Customer customer = _customerRepository.GetCustomerById(customerId);
            if (customer.Orders.Count == 0) 
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
            if (customer.Orders.Count == 0)
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
