using Microsoft.AspNetCore.Identity;
using TechStore.Domain.Entities.CartAggregate;
using TechStore.Domain.Entities.OrderAggregate;
using TechStore.Domain.Entities.WishlistAggregate;


namespace TechStore.Domain.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        // 1 - 1
        public int? RefreshTokenId { get; set; }
        public RefreshToken? RefreshToken { get; set; }
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
        public int? WishlistId { get; set; }
        public Wishlist? Wishlist { get; set; }

        // 1 - n
        public List<Order> Orders { get; set; }
    }
}
