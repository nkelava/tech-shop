using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.SubcategoryAggregate
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Slug), IsUnique = true)]
    public class Subcategory : Entity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? ImageURL { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // n - 1
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
