using JewelleryStoreManagementSystem.Data.Models;

#nullable enable
namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>
    {
        private new readonly JewelleryStoreManagementSystemContext _context;

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
