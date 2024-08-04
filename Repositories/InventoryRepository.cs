using JewelleryStoreManagementSystem.Models;

namespace JewelleryStoreManagementSystem.Repositories
{
    public class InventoryRepository : Repository<Inventory>
    {
        private readonly JewelleryStoreManagementSystemContext _context;

        public InventoryRepository(JewelleryStoreManagementSystemContext context) : base(context)
        {
            _context = context;
        }

    }
}



