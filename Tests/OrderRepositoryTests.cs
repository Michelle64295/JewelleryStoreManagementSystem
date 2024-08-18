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
    public class OrderRepositoryTests
    {
        private Mock<JewelleryStoreManagementSystemContext> _mockContext;
        private Mock<DbSet<Order>> _mockOrderSet;
        private OrderRepository _orderRepository;
        private IQueryable<Order> orders;
        private IQueryable<Customer> customers;

        [SetUp]
        public void Setup()
        {
            // Initialize mock data
            orders = new List<Order>
            {
                new Order { OrderId = 1, CustomerId = 1, OrderDate = new DateTime(2024, 1, 1) },
                new Order { OrderId = 2, CustomerId = 2, OrderDate = new DateTime(2024, 5, 12) }
            }.AsQueryable();

            customers = new List<Customer>
            {
               new Customer { CustomerId = 1, Password = "Spiderman", FullName = "Peter Parker", Email = "peter.parker@example.com", PhoneNumber = "0412345678", Address = "20 Ingram Street, Sydney, NSW" },
               new Customer { CustomerId = 2, Password = "IronMan!", FullName = "Tony Stark", Email = "tonystark29@example.com", PhoneNumber = "0476294719", Address = "80 Malibu Point, Sydney, NSW" }
            }.AsQueryable();

            // Setup mock DbSet for patients using helper method
            _mockOrderSet = MockDbSetHelper.CreateMockDbSet(orders);

            // Setup mock context
            _mockContext = new Mock<JewelleryStoreManagementSystemContext>();
            _mockContext.Setup(c => c.Orders).Returns(_mockOrderSet.Object);

            _orderRepository = new OrderRepository(_mockContext.Object);
        }

        [Test]
        public void GetOrderByCustomerId_ExistingCustomerId_ReturnsOrder()
        {
            var order = _orderRepository.GetOrderByCustomerId(1);

            Assert.IsNotNull(order);
            Assert.That(order.CustomerId, Is.EqualTo(1));
            Assert.That(order.OrderId, Is.EqualTo(1));
        }

        [Test]
        public void GetOrderByCustomerId_NonExistingCustomerId_ReturnsOrder()
        {
            var exception = Assert.Throws<NullReferenceException>(() => _orderRepository.GetOrderByCustomerId(5));
            Assert.That(exception.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

        [Test]
        public void GetAllOrders_ReturnsAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();

            Assert.That(orders.Count(), Is.EqualTo(2));
        }
    }
}
