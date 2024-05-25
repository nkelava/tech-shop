using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.ProductAggregate;


namespace TechStore.Domain.Entities.Cart
{
    [Index(nameof(Email), IsUnique = true)]
    public class Cart : Entity
    {
        public string Email { get; set; }
        public decimal TotalPrice { get; set; } = 0;

        // n - n
        public List<CartProduct> Products { get; set; }

        public void AddProduct(Product product, int quantity = 1, decimal unitPrice = 0)
        {
            var cartProduct = Products.FirstOrDefault(p => p.ProductId == product.Id);

            if (cartProduct is not null)
            {
                if (product.UnitsInStock > 0 && product.UnitsInStock >= quantity)
                {
                    TotalPrice += quantity > cartProduct.Quantity ? cartProduct.UnitPrice : -(cartProduct.UnitPrice);
                    cartProduct.Quantity = quantity;
                }

                return;
            }

            Products.Add(new CartProduct()
            {
                CartId = this.Id,
                ProductId = product.Id,
                Product = product,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = quantity * unitPrice
            });

            TotalPrice += quantity * unitPrice;
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
            TotalPrice = 0;
        }
    }
}
