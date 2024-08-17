using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;

namespace JewelleryStoreManagementSystem.Data.Services
{
    public class OrderService
    {
        private Order? _currentOrder;
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;

        public OrderService(ProductRepository productRepository, OrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _currentOrder = null;
        }

        public void AddToOrder(int productId, int quantity, int customerId)
        {
            if (_currentOrder == null)
            {
                _currentOrder = new Order
                {
                    OrderDate = DateTime.Now,
                    CustomerId = customerId
                };
                _orderRepository.AddOrder(_currentOrder);
            }

            Product? product = _productRepository.GetProductById(productId);
            OrderItem? existingOrderItem = _currentOrder.OrderItems.FirstOrDefault(x => x.ProductId == productId);

            if (existingOrderItem != null)
            {
                existingOrderItem.Quantity += quantity;
            }
            else
            {
                OrderItem orderItem = new OrderItem
                {
                    ProductId = product.ProductId,
                    Product = product,
                    Quantity = quantity,
                    OrderId = _currentOrder.OrderId,
                    Order = _currentOrder
                };

                _currentOrder.OrderItems.Add(orderItem);
            }

            _orderRepository.UpdateOrder(_currentOrder);

        }

        public Order GetOrder()
        {
            return _currentOrder ?? new Order();
        }

        public Order? GetOrderById(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        public void SetCustomer(Customer customer)
        {
            if (_currentOrder != null)
            {
                _currentOrder.CustomerId = customer.CustomerId;
                _currentOrder.Customer = customer;
                _orderRepository.UpdateOrder(_currentOrder);
            }
        }
    }
}
