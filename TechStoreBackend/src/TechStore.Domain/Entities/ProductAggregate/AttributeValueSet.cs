

namespace TechStore.Domain.Entities.ProductAggregate
{
    public class AttributeValueSet
    {
        // n - n
        public int AttributeId { get; set; }
        public ProductAttribute Attribute { get; set; }

        public int AttributeValueId { get; set; }
        public ProductAttributeValue AttributeValue { get; set; }
    }
}
