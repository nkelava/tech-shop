using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.ProductAggregate
{
    public class ProductAttributeValue : Entity
    {
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // n - n
        public List<ProductAttributeSet> ProductAttributes { get; set; }
    }
}
