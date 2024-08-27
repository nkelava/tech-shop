using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Domain.Entities.User;


namespace TechStore.Domain.Entities.CartAggregate
{
    public class Cart : Entity
    {
        public decimal TotalPrice { get; set; } = 0;

        // 1 - 1
        public string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        // n - n
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();


        public void AddProduct(Product product, int quantity = 1, decimal unitPrice = 0)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
            if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");

            var cartProduct = Products.FirstOrDefault(p => p.ProductId == product.Id);

            if (cartProduct != null)
            {
                if (product.UnitsInStock < quantity)
                    throw new InvalidOperationException("Not enough units in stock.");

                var quantityDifference = quantity - cartProduct.Quantity;
                cartProduct.Quantity = quantity;
                cartProduct.UnitPrice = unitPrice;
                cartProduct.TotalPrice = cartProduct.Quantity * cartProduct.UnitPrice;

                TotalPrice += quantityDifference * unitPrice;
                return;
            }

            if (product.UnitsInStock < quantity)
                throw new InvalidOperationException("Not enough units in stock.");

            var newCartProduct = new CartProduct()
            {
                CartId = this.Id,
                ProductId = product.Id,
                Product = product,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = quantity * unitPrice
            };

            Products.Add(newCartProduct);
            TotalPrice += quantity * unitPrice;
        }

        public int? RemoveProduct(int productId)
        {
            var cartProduct = Products.FirstOrDefault(p => p.ProductId == productId);

            if (cartProduct == null)
                return null;

            TotalPrice -= cartProduct.TotalPrice;
            Products.Remove(cartProduct);

            return cartProduct.ProductId;
        }

        public void Clear()
        {
            TotalPrice = 0;
            Products.Clear();
        }
    }
}
