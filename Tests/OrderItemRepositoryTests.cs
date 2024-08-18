using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
using JewelleryStoreManagementSystem.Data;
using JewelleryStoreManagementSystem.Tests.Util;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Numerics;
using Castle.Core.Resource;
#nullable enable

namespace JewelleryStoreManagementSystem.Tests
{
    [TestFixture]
    public class OrderItemRepositoryTests
    {
        private Mock<JewelleryStoreManagementSystemContext> _mockContext;
        private Mock<DbSet<OrderItem>> _mockOrderItemSet;
        private OrderItemRepository _orderItemRepository;
        private IQueryable<OrderItem> orderItems;
        private IQueryable<Order> orders;
        private IQueryable<Product> products;

        [SetUp]
        public void Setup()
        {
            // Initialize mock data
            orderItems = new List<OrderItem>
            {
                new OrderItem { OrderItemId = 1, Quantity = 5, OrderId = 1, ProductId = 1 },
                new OrderItem { OrderItemId = 2, Quantity = 2, OrderId = 1, ProductId = 2 }
            }.AsQueryable();

            orders = new List<Order>
            {
                new Order { OrderId = 1, CustomerId = 1, OrderDate = new DateTime(2024, 1, 1) },
                new Order { OrderId = 2, CustomerId = 2, OrderDate = new DateTime(2024, 5, 12) }
            }.AsQueryable();

            products = new List<Product>
            {
                new Product { ProductId = 1, Description = "The sleek design is immaculately crafted in 10kt yellow gold", ImagePath = "'~/images/diamond-ring.png", Name = "Diamond Ring in 10kt Yellow Gold", Price = 1599 },
                new Product { ProductId = 2, Description = "These exquisite gold earrings feature stunning blue details", ImagePath = "~/images/blue-earrings.png", Name = "Gold Earrings with Blue Details", Price = 899 }
            }.AsQueryable();

            // Setup mock DbSet for patients using helper method
            _mockOrderItemSet = MockDbSetHelper.CreateMockDbSet(orderItems);

            // Setup mock context
            _mockContext = new Mock<JewelleryStoreManagementSystemContext>();
            _mockContext.Setup(c => c.OrderItems).Returns(_mockOrderItemSet.Object);

            _orderItemRepository = new OrderItemRepository(_mockContext.Object);
        }

        [Test]
        public void GetAllOrderItemsInOrder_OrderWithOrderItems_ReturnsListOfOrderItems()
        {
            var orderItems = _orderItemRepository.GetAllOrderItemsInOrder(1);

            Assert.That(orderItems.Count(), Is.EqualTo(2));
            Assert.That(orderItems[0].OrderId, Is.EqualTo(1));
            Assert.That(orderItems[1].OrderId, Is.EqualTo(1));
        }

        [Test]
        public void GetAllOrderItemsInOrder_OrderWithoutAnyOrderItems_ReturnsEmptyList()
        {
            var orderItems = _orderItemRepository.GetAllOrderItemsInOrder(2);

            Assert.That(orderItems.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetAllOrderItemsInOrder_NonExistingOrderId_ReturnsEmptyList()
        {
            var orderItems = _orderItemRepository.GetAllOrderItemsInOrder(3);

            Assert.That(orderItems.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetOrderItemByOrderIdAndProductId_ExistingOrderIdAndExistingProductId_ReturnsOrderItem()
        {
            var orderItem = _orderItemRepository.GetOrderItemByOrderIdAndProductId(1, 1);

            Assert.IsNotNull(orderItem);
            Assert.That(orderItem.OrderItemId, Is.EqualTo(1));
        }

        [Test]
        public void GetOrderItemByOrderIdAndProductId_NonExistingOrderIdAndExistingProductId_ReturnsNull()
        {
            var orderItem = _orderItemRepository.GetOrderItemByOrderIdAndProductId(3, 1);

            Assert.IsNull(orderItem);
        }

        [Test]
        public void GetOrderItemByOrderIdAndProductId_ExistingOrderIdAndNonExistingProductId_ReturnsNull()
        {
            var orderItem = _orderItemRepository.GetOrderItemByOrderIdAndProductId(1, 5);

            Assert.IsNull(orderItem);
        }

        [Test]
        public void GetOrderItemByOrderIdAndProductId_NonExistingOrderIdAndNonExistingProductId_ReturnsNull()
        {
            var orderItem = _orderItemRepository.GetOrderItemByOrderIdAndProductId(5, 5);

            Assert.IsNull(orderItem);
        }
    }
}
