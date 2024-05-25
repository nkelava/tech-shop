using TechStore.Application.Models.Attribute;
using TechStore.Application.Models.AttributeValue;


namespace TechStore.Application.Models.Product
{
    public class ProductAttributeSetModel
    {
        public int AttributeId { get; set; }
        public AttributeReadModel Attribute { get; set; }

        public int AttributeValueId { get; set; }
        public AttributeValueReadModel AttributeValue { get; set; }
    }
}
