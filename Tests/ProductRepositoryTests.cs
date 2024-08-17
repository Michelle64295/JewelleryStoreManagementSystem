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
    public class ProductRepositoryTests
    {
        private Mock<JewelleryStoreManagementSystemContext> _mockContext;
        private Mock<DbSet<Product>> _mockProductSet;
        private ProductRepository _productRepository;
        private IQueryable<Product> products;

        [SetUp]
        public void Setup()
        {
            // Initialize mock data
            products = new List<Product>
            {
                new Product { ProductId = 1, Description = "The sleek design is immaculately crafted in 10kt yellow gold", ImagePath = "'~/images/diamond-ring.png", Name = "Diamond Ring in 10kt Yellow Gold", Price = "$1,599" },
                new Product { ProductId = 2, Description = "These exquisite gold earrings feature stunning blue details", ImagePath = "~/images/blue-earrings.png", Name = "Gold Earrings with Blue Details", Price = "$899" }
            }.AsQueryable();


            // Setup mock DbSet for patients using helper method
            _mockProductSet = MockDbSetHelper.CreateMockDbSet(products);

            // Setup mock context
            _mockContext = new Mock<JewelleryStoreManagementSystemContext>();
            _mockContext.Setup(c => c.Products).Returns(_mockProductSet.Object);

            _productRepository = new ProductRepository(_mockContext.Object);
        }

        [Test]
        public void GetAllProducts_ReturnsAllProducts()
        {
            var products = _productRepository.GetAllProducts();

            Assert.That(products.Count(), Is.EqualTo(2));
        }
    }
}
