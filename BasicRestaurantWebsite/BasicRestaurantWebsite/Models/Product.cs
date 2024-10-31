namespace BasicRestaurantWebsite.Models
{
    public class Product
    {
        public int ProductId { get; set; } //PK
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; } // Foreign Key
        public Category? Category { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; } // Product can be in many order items
        public ICollection<ProductIngredient>? ProductIngredients { get; set; } // Multiple ingredients per product
    }
}
