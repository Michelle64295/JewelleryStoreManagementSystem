#nullable enable
namespace JewelleryStoreManagementSystem.Data.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
