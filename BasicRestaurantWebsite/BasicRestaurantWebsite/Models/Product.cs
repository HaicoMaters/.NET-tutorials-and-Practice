using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BasicRestaurantWebsite.Models
{
    public class Product
    {
        public Product()
        {
            ProductIngredients = new List<ProductIngredient>();
        }

        public int ProductId { get; set; } //PK
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; } // Foreign Key

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";

        // Navigation Properties Stop Validation Errors
        [ValidateNever] 
        public Category? Category { get; set; } // Product belongs to a category
        [ValidateNever]
        public ICollection<OrderItem>? OrderItems { get; set; } // Product can be in many order items
        [ValidateNever]
        public ICollection<ProductIngredient>? ProductIngredients { get; set; } // Multiple ingredients per product
    }
}
