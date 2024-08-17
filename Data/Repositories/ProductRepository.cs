using JewelleryStoreManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        private new readonly JewelleryStoreManagementSystemContext _context;

        public ProductRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            Product? product = _context.Products.FirstOrDefault(c => c.ProductId == id);
            if (product == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return product;
            }
        }

        public Product GetProductByName(string name)
        {
            Product? product = _context.Products.FirstOrDefault(c => c.Name == name);
            if (product == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return product;
            }
        }
    }
}
