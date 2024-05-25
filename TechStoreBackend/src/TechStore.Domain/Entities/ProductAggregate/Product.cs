using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Entities.Base;
using TechStore.Domain.Entities.Cart;
using TechStore.Domain.Entities.OrderAggregate;
using TechStore.Domain.Entities.SubcategoryAggregate;
using TechStore.Domain.Entities.Wishlist;


namespace TechStore.Domain.Entities.ProductAggregate
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? ImageURL { get; set; } = null;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public int Discount { get; set; } = 0;
        public bool OnSale { get; set; } = false;
        public int UnitsInStock { get; set; } = 0;
        public int UnitsSold { get; set; } = 0;
        public decimal Rating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // n - 1
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public int? PromoCodeId { get; set; }
        public PromoCode PromoCode {  get; set; }

        //// 1 - n
        public List<Review> Reviews { get; set; }

        //// n - n
        public List<ProductAttributeSet> ProductAttributes { get; set; }
        public List<OrderProduct> Orders { get; set; }
        public List<CartProduct> Carts { get; set; }
        public List<WishlistProduct> WishLists { get; set; }


        public void AddReview(Review review)
        {
            if (review is null) return;

            Reviews.Add(review);
            ReviewCount++;

            decimal newRating = Rating * (ReviewCount - 1) + review.Rate;
            Rating = Math.Round(newRating / ReviewCount, 1);
        }

        public void RemoveReview(int reviewId)
        {
            var reviewToRemove = Reviews.FirstOrDefault(r => r.Id == reviewId);

            if (reviewToRemove == null) return;

            Reviews.Remove(reviewToRemove);
            ReviewCount--;

            if (ReviewCount > 0) {
                decimal totalRating = Reviews.Sum(r => r.Rate);
                Rating = Math.Round(totalRating / ReviewCount, 1);
            } else {
                Rating = 0;
            }
        }
    }
}