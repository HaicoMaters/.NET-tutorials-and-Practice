namespace BasicRestaurantWebsite.Models
{
    public class Order // Domain Model
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public int OrderId { get; set; } //PK
        public DateTime OrderDate { get; set; }
        public string? UserID { get; set; } // Foreign Key
        public ApplicationUser User { get; set; }
        public decimal TotalAmount {  get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
