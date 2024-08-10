#nullable enable
namespace JewelleryStoreManagementSystem.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public string Price { get; set; }
        public int Stock {  get; set; }
        public string ImagePath { get; set; }
    }
}
