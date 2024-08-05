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

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public bool IsValidCustomerCredentials(string email, string password)
        {
            return GetAllCustomers().Any(c => c.Email == email && c.Password == password);
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _context.Customers.SingleOrDefault(c => c.Email == email);
        }

    }
}
