using TechStore.Domain.Entities.Base;

namespace TechStore.Domain.Entities.ProductAggregate
{
    public class ProductAttribute : Entity
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // n - n
        public List<AttributeValueSet> AttributeValues { get; set; }
        public List<ProductAttributeSet> ProductAttributes { get; set; }
    }
}
