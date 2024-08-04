using JewelleryStoreManagementSystem.Data.Models;

namespace JewelleryStoreManagementSystem.Data.Repositories
{
    public class ReviewRepository : Repository<Review>
    {
        private readonly JewelleryStoreManagementSystemContext _context;

        public ReviewRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

    }
}
