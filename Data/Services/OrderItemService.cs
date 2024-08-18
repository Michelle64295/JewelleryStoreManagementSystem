using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
#nullable enable

namespace JewelleryStoreManagementSystem.Data.Services
{
    public class OrderItemService
    {
        private readonly ProductRepository _productRepository;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly CustomerRepository _customerRepository;

        public OrderItemService(ProductRepository productRepository, OrderItemRepository orderItemRepository, CustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
            _customerRepository = customerRepository;
        }

        public void AddOrderItem(int quantity, int orderId, int productId)
        {
            OrderItem? existingOrderItem = _orderItemRepository.GetOrderItemByOrderIdAndProductId(orderId, productId);

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

