using JewelleryStoreManagementSystem.Models;

namespace JewelleryStoreManagementSystem.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        private readonly JewelleryStoreManagementSystemContext _context;

        public OrderRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

    }
}
