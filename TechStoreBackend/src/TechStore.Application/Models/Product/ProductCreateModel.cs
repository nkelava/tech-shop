using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.Product
{
    public class ProductCreateModel
    {
        [Required(ErrorMessage = "Please provide product name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide product slug.")]
        [RegularExpression(@"^[a-z0-9]+(-[a-z0-9]+)*$", ErrorMessage = "Invalid slug format. Use lowercase letters, numbers and hyphens (e.g., 'this-is-a-slug-123').")]
        public string Slug { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal? Price { get; set; }
        
        public bool? OnSale { get; set; }

        [Url(ErrorMessage = "Invalid image URL format.")]
        public string? ImageURL { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Units in stock cannot be negative.")]
        public int? UnitsInStock { get; set; }


        public int? PromoCodeId { get; set; }

        [Required(ErrorMessage = "Subcategory ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Subcategory ID must be greater than zero.")]
        public int SubcategoryId { get; set; }
    }
}
