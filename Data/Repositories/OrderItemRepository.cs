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

        public List<OrderItem> GetAllOrderItemsInOrder(int orderId)
        {
            return _context.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
        }

        public OrderItem? GetOrderItemByOrderIdAndProductId(int orderId, int productId)
        {
            return _context.OrderItems
                           .FirstOrDefault(o => o.OrderId == orderId && o.ProductId == productId);
        }
    }
}
