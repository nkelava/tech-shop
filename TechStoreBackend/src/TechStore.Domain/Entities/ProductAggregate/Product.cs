using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.Cart;
using TechStore.Domain.Entities.Order;
using TechStore.Domain.Entities.SubcategoryAggregate;
using TechStore.Domain.Entities.Wishlist;


namespace TechStore.Domain.Entities.ProductAggregate
{
    public class Product : Entity
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

        public int UnitsSold { get; set; } = 0;

        public decimal Rating { get; set; } = 0;

        public int ReviewCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // n - 1
        public Brand Brand { get; set; }

        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        //// 1 - n
        public IList<Review> Reviews { get; set; }

        //// n - n
        public IList<ProductProperty> Properties { get; set; }
        public IList<OrderProduct> Orders { get; set; }
        public IList<CartProduct> Carts { get; set; }
        public IList<WishListProduct> WishLists { get; set; }
    }
}