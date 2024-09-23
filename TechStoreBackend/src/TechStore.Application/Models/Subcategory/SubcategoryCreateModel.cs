using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TechStore.Application.Models.Subcategory
{
    public class SubcategoryCreateModel
    {
        [Required(ErrorMessage = "Please provide a name.")]
        [MaxLength(48, ErrorMessage = "Name must be 48 characters or fewer.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Name can only contain alphabetic characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a slug.")]
        [MaxLength(48, ErrorMessage = "Slug must be 48 characters or fewer.")]
        [RegularExpression(@"^[a-z0-9]+(-[a-z0-9]+)*$", ErrorMessage = "Invalid slug format. Use lowercase letters, numbers and hyphens (e.g., 'this-is-a-slug-123').")]
        public string Slug { get; set; }
        public string? ImageURL { get; set; } = null;
        [NotMapped]
        public IFormFile? Image { get; set; } = null;
        public byte[]? ImageByte { get; set; }

        [Required(ErrorMessage = "Please specify a category.")]
        public int CategoryId { get; set; }
    }
}
