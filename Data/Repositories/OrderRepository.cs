using JewelleryStoreManagementSystem.Data.Models;

#nullable enable
namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        private new readonly JewelleryStoreManagementSystemContext _context;

        public OrderRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }
    }
}
