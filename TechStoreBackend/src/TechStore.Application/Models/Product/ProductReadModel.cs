using TechStore.Application.Models.Base;


namespace TechStore.Application.Models.Product
{
    public class ProductReadModel : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string Summary { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public string ImageURL { get; set; }
        public int UnitsInStock { get; set; } = 0;
        public decimal Rating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;

        public List<ProductAttributeSetModel> ProductAttributes { get; set; }
    }
}
