using JewelleryStoreManagementSystem.Models;

namespace JewelleryStoreManagementSystem.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        private readonly JewelleryStoreManagementSystemContext _context;

        public ProductRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

    }
}
