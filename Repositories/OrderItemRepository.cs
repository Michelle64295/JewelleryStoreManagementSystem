using JewelleryStoreManagementSystem.Models;

namespace JewelleryStoreManagementSystem.Repositories
{
    public class OrderItemRepository : Repository<OrderItem>
    {
        private readonly JewelleryStoreManagementSystemContext _context;

        public OrderItemRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

    }
}
