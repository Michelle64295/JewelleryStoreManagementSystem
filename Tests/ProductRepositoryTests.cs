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
                new Product { ProductId = 1, Description = "The sleek design is immaculately crafted in 10kt yellow gold", ImagePath = "'~/images/diamond-ring.png", Name = "Diamond Ring in 10kt Yellow Gold", Price = 1599 },
                new Product { ProductId = 2, Description = "These exquisite gold earrings feature stunning blue details", ImagePath = "~/images/blue-earrings.png", Name = "Gold Earrings with Blue Details", Price = 899 }
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

        [Test]
        public void GetProductById_ExistingProductId_ReturnsProduct()
        {
            var product = _productRepository.GetProductById(1);

            Assert.IsNotNull(product);
            Assert.That(product.ProductId, Is.EqualTo(1));
            Assert.That(product.Description, Is.EqualTo("The sleek design is immaculately crafted in 10kt yellow gold"));
        }

        [Test]
        public void GetProductById_NonExistingId_ThrowsException()
        {
            var exception = Assert.Throws<NullReferenceException>(() => _productRepository.GetProductById(10));
            Assert.That(exception.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

        [Test]
        public void GetProductByName_ExistingProductName_ReturnsProduct()
        {
            var product = _productRepository.GetProductByName("Diamond Ring in 10kt Yellow Gold");

            Assert.IsNotNull(product);
            Assert.That(product.ProductId, Is.EqualTo(1));
            Assert.That(product.Description, Is.EqualTo("The sleek design is immaculately crafted in 10kt yellow gold"));
        }

        [Test]
        public void GetProductByName_NonExistingName_ThrowsException()
        {
            var exception = Assert.Throws<NullReferenceException>(() => _productRepository.GetProductByName("Fake Name"));
            Assert.That(exception.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }
    }
}
