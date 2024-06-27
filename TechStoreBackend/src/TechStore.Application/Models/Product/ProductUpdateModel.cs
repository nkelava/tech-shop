

namespace TechStore.Application.Models.Product
{
    public class ProductUpdateModel
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;
        public decimal? Price { get; set; } = 0;
        public bool? OnSale { get; set; } = false;

        public string ImageURL { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int UnitsInStock { get; set; } = 0;
        
        public int Discount { get; set; } = 0;

        public int? PromoCodeId { get; set; }
        public int SubcategoryId { get; set; }
    }
}
