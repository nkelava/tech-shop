

namespace TechStore.Application.Models.Product
{
    public class ProductCreateModel
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string ImageURL { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Discount { get; set; } = 0;

        public bool OnSale { get; set; } = false;

        public decimal SalePrice { get; set; } = 0;

        public decimal Price { get; set; } = 0;

        public int UnitsInStock { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int BrandId { get; set; }
        public int SubcategoryId { get; set; }

        public IList<ProductPropertyModel> Properties { get; set; }
    }
}
