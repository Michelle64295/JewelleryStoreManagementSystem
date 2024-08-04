namespace JewelleryStoreManagementSystem.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
