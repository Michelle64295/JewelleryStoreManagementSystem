using JewelleryStoreManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToList();
        }

        public Order GetOrderByCustomerId(int customerId)
        {
            Order? order =  GetAllOrders().Where(o => o.CustomerId == customerId).FirstOrDefault();
            if (order == null) {
                throw new NullReferenceException();
            }
            else
            {
                return order;
            }
        }
    }
}
