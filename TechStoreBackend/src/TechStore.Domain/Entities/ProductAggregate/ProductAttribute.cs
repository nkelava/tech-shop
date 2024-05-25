using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Entities.Base;

namespace TechStore.Domain.Entities.ProductAggregate
{
    [Index(nameof(Name), IsUnique = true)]
    public class ProductAttribute : Entity
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // n - n
        public List<ProductAttributeSet> ProductAttributes { get; set; }
    }
}
