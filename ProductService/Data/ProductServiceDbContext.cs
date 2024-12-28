using Microsoft.EntityFrameworkCore;
using EMall.Shared;
namespace ProductService.Data
{
    public class ProductServiceDbContext : DbContext
    {
        public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> Options) : base(Options)
        {
            
        }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);

            ModelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
        }
    }
}
