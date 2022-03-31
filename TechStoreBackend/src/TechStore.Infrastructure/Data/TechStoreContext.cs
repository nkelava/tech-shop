using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Entities;


namespace TechStore.Infrastructure.Data
{
    public  class TechStoreContext : DbContext
    {
        public TechStoreContext(DbContextOptions<TechStoreContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
