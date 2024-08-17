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
            return _context.Orders.ToList();
        }

        public Order? GetOrderByCustomerId(int customerId)
        {
            return _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).Where(o => o.CustomerId == customerId).FirstOrDefault();
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder (Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public Order? GetOrderById(int orderId)
        {
            return _context.Orders.Include(o => o.OrderItems).ThenInclude(o => o.Product).SingleOrDefault(o => o.OrderId == orderId);
        }
    }
}
