using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStore.Domain.Entities;
using TechStore.Domain.Entities.Cart;
using TechStore.Domain.Entities.Order;
using TechStore.Domain.Entities.ProductAggregate;
using TechStore.Domain.Entities.SubcategoryAggregate;
using TechStore.Domain.Entities.Wishlist;


namespace TechStore.Infrastructure.Data
{
    public  class TechStoreContext : DbContext
    {
        public TechStoreContext(DbContextOptions<TechStoreContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<SubcategoryProperty> SubcategoryProperties { get; set; }
        public DbSet<Wishlist> WishLists { get; set; }
        public DbSet<WishListProduct> WishListProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            SetTableNamesAsSingle(builder);

            base.OnModelCreating(builder);

            builder.Entity<ProductProperty>(ConfigureProductProperties);
            builder.Entity<SubcategoryProperty>(ConfigureSubcategoryProperties);
            builder.Entity<WishListProduct>(ConfigureWishListProducts);
            builder.Entity<OrderProduct>(ConfigureOrderProducts);
            builder.Entity<CartProduct>(ConfigureCartProducts);
        }

        private static void SetTableNamesAsSingle(ModelBuilder builder)
        {
            // Instead of the Context.DbSet<T> name use the entity name
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                builder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }
        }
        
        private void ConfigureProductProperties(EntityTypeBuilder<ProductProperty> builder)
        {
            builder.HasKey(pa => new { pa.ProductId, pa.PropertyId });
        }

        private void ConfigureSubcategoryProperties(EntityTypeBuilder<SubcategoryProperty> builder)
        {
            builder.HasKey(sa => new { sa.SubcategoryId, sa.PropertyId });
        }

        private void ConfigureWishListProducts(EntityTypeBuilder<WishListProduct> builder)
        {
            builder.HasKey(wp => new { wp.WishListId, wp.ProductId });
        }

        private void ConfigureOrderProducts(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => new { op.OrderId, op.ProductId });
        }

        private void ConfigureCartProducts(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasKey(cp => new { cp.CartId, cp.ProductId });
        }
    }
}
