namespace JewelleryStoreManagementSystem.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int StockQuanity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
