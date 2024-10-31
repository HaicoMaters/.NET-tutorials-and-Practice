namespace BasicRestaurantWebsite.Models
{
    public class Category
    {
        public int CategoryId { get; set; } // PK
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}