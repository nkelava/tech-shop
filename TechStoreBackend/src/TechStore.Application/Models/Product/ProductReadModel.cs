using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using TechStore.Application.Models.Base;
using TechStore.Application.Models.Subcategory;


namespace TechStore.Application.Models.Product
{
    public class ProductReadModel : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string ImageURL { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public byte[] ImageByte { get; set; }

        public string Summary { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public int Discount { get; set; } = 0;
        public bool OnSale { get; set; } = false;
        public int UnitsInStock { get; set; } = 0;
        public decimal Rating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;

        public SubcategoryReadModel? Subcategory { get; set; }

        public List<ProductAttributeSetModel>? ProductAttributes { get; set; }
    }
}
