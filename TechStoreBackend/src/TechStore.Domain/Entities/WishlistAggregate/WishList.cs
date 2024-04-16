using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.Wishlist
{
    public class Wishlist : Entity
    {
        public string Email { get; set; }

        // n - n
        public List<WishListProduct> Products { get; set; } = new List<WishListProduct>();


        public void AddProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.ProductId == productId);

            if (product is not null)
                return;

            Products.Add(new WishListProduct
            {
                WishListId = this.Id,
                ProductId = productId
            });
        }

        public void RemoveProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.ProductId == productId);

            if(product is not null)
            {
                Products.Remove(product);
            }
        }
    }
}
