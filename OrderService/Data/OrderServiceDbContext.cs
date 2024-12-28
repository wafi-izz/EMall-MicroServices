using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using EMall.Shared;

namespace OrderService.Data
{
    public class OrderServiceDbContext : DbContext
    {
        public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> Options) : base(Options)
        {
            
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);

            ModelBuilder.Entity<Order>().Property(p => p.TotalPrice).HasPrecision(18, 2);
            ModelBuilder.Entity<OrderItem>().Property(p => p.Price).HasPrecision(18, 2);
        }
    }
}
