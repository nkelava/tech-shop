using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Domain.Entities.User;


namespace TechStore.Domain.Entities.WishlistAggregate
{
    public class Wishlist : Entity
    {
        // 1 - 1
        public string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        // n - n
        public List<WishlistProduct> Products { get; set; } = new List<WishlistProduct>();


        public void AddProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            var wishlistProduct = Products.FirstOrDefault(p => p.ProductId == product.Id);

            if (wishlistProduct != null)
                return;

            Products.Add(new WishlistProduct
            {
                WishlistId = this.Id,
                ProductId = product.Id,
                Product = product
            }); ;
        }

        public int? RemoveProduct(int productId)
        {
            var wishlishProduct = Products.FirstOrDefault(p => p.ProductId == productId);

            if (wishlishProduct == null)
                return null;

            Products.Remove(wishlishProduct);

            return productId;
        }

        public void Clear()
        {
            Products.Clear();
        }
    }
}
