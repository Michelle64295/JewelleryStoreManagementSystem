#nullable enable
namespace JewelleryStoreManagementSystem.Data.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public ICollection<Order>? Orders { get; set; }
    }
}
