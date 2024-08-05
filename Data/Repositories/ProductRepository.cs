using JewelleryStoreManagementSystem.Data.Models;

namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        private readonly JewelleryStoreManagementSystemContext _context;

        public ProductRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductByName(string name)
        {
            return _context.Products.SingleOrDefault(c => c.Name == name);
        }
    }
}
