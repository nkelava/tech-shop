using TechStore.Domain.Entities.Base;


namespace TechStore.Domain.Entities.Cart
{
    public class Cart : Entity
    {
        public string Username { get; set; }
        public decimal TotalPrice { get; set; } = 0;

        // n - n
        public IList<CartProduct> Products { get; set; }

        public void AddProduct(int productId, int quantity = 1, decimal unitPrice = 0)
        {
            var product = Products.FirstOrDefault(p => p.ProductId == productId);

            if(product != null)
            {
                ++product.Quantity;
                product.TotalPrice = product.Quantity * product.UnitPrice;
                //product.TotalPrice += product.UnitPrice;
                return;
            }

            Products.Add(new CartProduct()
            {
                CartId = this.Id,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = quantity * unitPrice
            });
        }

        public void RemoveProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.ProductId == productId);

            if(product != null)
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
