using JewelleryStoreManagementSystem.Data.Models;

namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public class AdminRepository : Repository<Admin>
    {
        private readonly JewelleryStoreManagementSystemContext _context;
        public AdminRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Admin> GetAllAdmins()
        {
            return _context.Admins.ToList();
        }

        public bool IsValidAdminCredentials(string email, string password)
        {
            return GetAllAdmins().Any(c => c.Email == email && c.Password == password);
        }

        public Admin GetAdminByEmail(string email)
        {
            return _context.Admins.SingleOrDefault(c => c.Email == email);
        }
    }
}
