using System.ComponentModel.DataAnnotations;


namespace TechStore.Application.Models.Category
{
    public class CategoryCreateModel
    {
        [Required(ErrorMessage = "Please provide a name.")]
        [MaxLength(48, ErrorMessage = "Name  must be 48 characters or fewer.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain alphabetic characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a slug.")]
        [MaxLength(48, ErrorMessage = "Slug must be 48 characters or fewer.")]
        [RegularExpression(@"^[a-z]+(-[a-z]+)*$", ErrorMessage = "Invalid slug format. Use lowercase letters and hyphens (e.g., 'this-is-a-slug').")]
        public string Slug { get; set; }
    }
}
