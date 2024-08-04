using JewelleryStoreManagementSystem.Data.Models;

namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        private readonly JewelleryStoreManagementSystemContext _context;

        public CustomerRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }
        public List<(int id, string password)> GetCustomerCredentials()
        {
            return _context.Customers
                           .Select(c => new { c.CustomerId, c.Password })
                           .ToList()
                           .Select(c => (c.CustomerId, c.Password))
                           .ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.SingleOrDefault(c => c.CustomerId == id);
        }

    }
}
