

namespace TechStore.Application.Models.Product
{
    public class ProductCreateModel
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public decimal? Price { get; set; }
        
        public bool? OnSale { get; set; }

        public string? ImageURL { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public int? UnitsInStock { get; set; }


        public int? PromoCodeId { get; set; }
        public int SubcategoryId { get; set; }
    }
}
