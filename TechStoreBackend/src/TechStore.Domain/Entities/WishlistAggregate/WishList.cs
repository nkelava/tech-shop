using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.ProductAggregate;

namespace TechStore.Domain.Entities.Wishlist
{
    [Index(nameof(Email), IsUnique = true)]
    public class Wishlist : Entity
    {
        public string Email { get; set; }

        // n - n
        public List<WishlistProduct> Products { get; set; } = new List<WishlistProduct>();


        public void AddProduct(Product product)
        {
            var wishlistProduct = Products.FirstOrDefault(p => p.ProductId == product.Id);

            if (wishlistProduct is not null)
                return;

            Products.Add(new WishlistProduct
            {
                WishlistId = this.Id,
                ProductId = product.Id,
                Product = product
            }); ;
        }

        public void RemoveProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.ProductId == productId);

            if (product is not null)
            {
                Products.Remove(product);
            }
        }

        public void Clear()
        {
            Products.Clear();
        }
    }
}
