using JewelleryStoreManagementSystem.Data.Models;

namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>
    {
        private readonly JewelleryStoreManagementSystemContext _context;

        public OrderItemRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return _context.OrderItems.ToList();
        }
    }
}
