using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStore.Application.Models.Authorization;
using TechStore.Domain.Entities;
using TechStore.Domain.Entities.Cart;
using TechStore.Domain.Entities.OrderAggregate;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Domain.Entities.SubcategoryAggregate;
using TechStore.Domain.Entities.User;
using TechStore.Domain.Entities.Wishlist;


namespace TechStore.Infrastructure.Data
{
    public  class TechStoreContext : IdentityDbContext<ApplicationUser>
    {
        public TechStoreContext(DbContextOptions<TechStoreContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> Attributes { get; set; }
        public DbSet<ProductAttributeValue> AttributeValues { get; set; }
        public DbSet<ProductAttributeSet> ProductAttributes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Wishlist> WishLists { get; set; }
        public DbSet<WishlistProduct> WishListProducts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            SetTableNamesAsSingle(builder);

            base.OnModelCreating(builder);

            builder.Entity<ProductAttributeSet>(ConfigureProductAttributes);
            builder.Entity<CartProduct>(ConfigureCartProducts);
            builder.Entity<WishlistProduct>(ConfigureWishListProducts);
            builder.Entity<OrderProduct>(ConfigureOrderProducts);
        }

        private static void SetTableNamesAsSingle(ModelBuilder builder)
        {
            // Instead of the Context.DbSet<T> name use the entity name
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                builder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }
        }

        private void ConfigureProductAttributes(EntityTypeBuilder<ProductAttributeSet> builder)
        {
            builder.HasKey(pav => new { pav.ProductId, pav.AttributeId, pav.AttributeValueId });
        }

        private void ConfigureCartProducts(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasKey(cp => new { cp.CartId, cp.ProductId });
        }

        private void ConfigureWishListProducts(EntityTypeBuilder<WishlistProduct> builder)
        {
            builder.HasKey(wp => new { wp.WishlistId, wp.ProductId });
        }

        private void ConfigureOrderProducts(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => new { op.OrderId, op.ProductId });

            builder.HasOne(o => o.Order).WithMany(op => op.Products).HasForeignKey(o => o.OrderId);
            builder.HasOne(p => p.Product).WithMany(op => op.Orders).HasForeignKey(p => p.ProductId);
        }
    }
}
