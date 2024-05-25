

namespace TechStore.Application.Models.Product
{
    public class ProductSpecificationModel
    {
        public int ProductId { get; set; }

        public List<ProductAttributeSetModel> ProductAttributes { get; set; }
    }
}
