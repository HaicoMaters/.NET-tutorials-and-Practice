namespace BasicRestaurantWebsite.Models
{
    // Join Table for Product and Ingrident Many-to-Many Relationship
    public class ProductIngredient
    {
        public int ProductId { get; set; } //PK
        public Product Product { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set;}

    }
}