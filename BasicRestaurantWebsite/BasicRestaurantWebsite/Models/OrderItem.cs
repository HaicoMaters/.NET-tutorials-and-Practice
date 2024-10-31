namespace BasicRestaurantWebsite.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; } // PK
        public int OrderID { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; } //Foreign Key
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set;}
    }
}
