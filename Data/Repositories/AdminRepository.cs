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
        public List<(int id, string password)> GetAdminCredentials()
        {
            return _context.Admins
                           .Select(a => new { a.AdminId, a.Password })
                           .ToList()
                           .Select(a => (a.AdminId, a.Password))
                           .ToList();
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admins.SingleOrDefault(a => a.AdminId == id);
        }
    }
}
