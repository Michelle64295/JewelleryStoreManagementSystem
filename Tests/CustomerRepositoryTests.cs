using JewelleryStoreManagementSystem.Data;
using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
using JewelleryStoreManagementSystem.Tests.Util;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
#nullable enable

namespace JewelleryStoreManagementSystem.Tests
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        private Mock<JewelleryStoreManagementSystemContext> _mockContext;
        private Mock<DbSet<Customer>> _mockCustomerSet;
        private CustomerRepository _customerRepository;
        private IQueryable<Customer> customers;

        [SetUp]
        public void Setup()
        {
            // Initialize mock data
            customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Password = "Spiderman", FullName = "Peter Parker", Email = "peter.parker@example.com", PhoneNumber = "0412345678", Address = "20 Ingram Street, Sydney, NSW" },
                new Customer { CustomerId = 2, Password = "IronMan!", FullName = "Tony Stark", Email = "tonystark29@example.com", PhoneNumber = "0476294719", Address = "80 Malibu Point, Sydney, NSW" }
            }.AsQueryable();


            // Setup mock DbSet for patients using helper method
            _mockCustomerSet = MockDbSetHelper.CreateMockDbSet(customers);

            // Setup mock context
            _mockContext = new Mock<JewelleryStoreManagementSystemContext>();
            _mockContext.Setup(c => c.Customers).Returns(_mockCustomerSet.Object);

            _customerRepository = new CustomerRepository(_mockContext.Object);
        }

        [Test]
        public void GetAllCustomers_ReturnsAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();

            Assert.That(customers.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IsValidCustomerCredentials_ValidEmailAndValidPassword_ReturnsTrue()
        {
            var customer = _customerRepository.IsValidCustomerCredentials("peter.parker@example.com", "Spiderman");

            Assert.IsTrue(customer);
        }

        [Test]
        public void IsValidCustomerCredentials_ValidEmailAndInvalidPassword_ReturnsFalse()
        {
            var customer = _customerRepository.IsValidCustomerCredentials("peter.parker@example.com", "NotSpiderman");

            Assert.IsFalse(customer);
        }

        [Test]
        public void IsValidCustomerCredentials_InvalidCredentials_ReturnsFalse()
        {
            var customer = _customerRepository.IsValidCustomerCredentials("something@gmail.com", "Password");

            Assert.IsFalse(customer);
        }

        [Test]
        public void GetAllCustomersByEmail_ReturnsAllCustomersEmailOnly()
        {
            var customersEmail = _customerRepository.GetAllCustomersByEmail();

            Assert.That(customers.Count(), Is.EqualTo(2));
            Assert.Contains("peter.parker@example.com", customersEmail.ToList());
            Assert.Contains("tonystark29@example.com", customersEmail.ToList());
        }

        [Test]
        public void GetCustomerById_ExistingId_ReturnsCustomer()
        {
            var customer = _customerRepository.GetCustomerById(1);

            Assert.IsNotNull(customer);
            Assert.That(customer.CustomerId, Is.EqualTo(1));
            Assert.That(customer.FullName, Is.EqualTo("Peter Parker"));
        }

        [Test]
        public void GetCustomerById_NonExistingId_ThrowsException()
        {
            var exception = Assert.Throws<NullReferenceException>(() => _customerRepository.GetCustomerById(11));
            Assert.That(exception.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

        [Test]
        public void GetCustomerByEmail_ExistingEmail_ReturnsCustomer()
        {
            var customer = _customerRepository.GetCustomerByEmail("peter.parker@example.com");

            Assert.IsNotNull(customer);
            Assert.That(customer.CustomerId, Is.EqualTo(1));
            Assert.That(customer.Email, Is.EqualTo("peter.parker@example.com"));
        }

        [Test]
        public void GetCustomerByEmail_NonExistingEmail_ThrowsException()
        {
            var exception = Assert.Throws<NullReferenceException>(() => _customerRepository.GetCustomerByEmail("hello"));
            Assert.That(exception.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }
    }
}
