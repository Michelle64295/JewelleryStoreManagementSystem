using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
using JewelleryStoreManagementSystem.Data;
using JewelleryStoreManagementSystem.Tests.Util;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
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

        [SetUp]
        public void Setup()
        {
            // Initialize mock data
            orderItems = new List<OrderItem>
            {
                new OrderItem { OrderItemId = 1, Quantity = 5, OrderId = 1, ProductId = 1 },
                new OrderItem { OrderItemId = 2, Quantity = 2, OrderId = 5, ProductId = 1 }
            }.AsQueryable();


            // Setup mock DbSet for patients using helper method
            _mockOrderItemSet = MockDbSetHelper.CreateMockDbSet(orderItems);

            // Setup mock context
            _mockContext = new Mock<JewelleryStoreManagementSystemContext>();
            _mockContext.Setup(c => c.OrderItems).Returns(_mockOrderItemSet.Object);

            _orderItemRepository = new OrderItemRepository(_mockContext.Object);
        }

        [Test]
        public void GetAllOrderItems_ReturnsAllOrderItems()
        {
            var orderItems = _orderItemRepository.GetAllOrderItems();

            Assert.That(orderItems.Count(), Is.EqualTo(2));
        }
    }
}
