using JewelleryStoreManagementSystem.Data.Models;

namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        private readonly JewelleryStoreManagementSystemContext _context;

        public OrderRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrder()
        {
            return _context.Orders.ToList();
        }
    }
}
