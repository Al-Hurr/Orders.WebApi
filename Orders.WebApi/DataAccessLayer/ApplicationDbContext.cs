using Microsoft.EntityFrameworkCore;
using Orders.WebApi.Domain.Orders.Entities;
using Orders.WebApi.Domain.Products.Entities;

namespace Orders.WebApi.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
            .HasOne<Order>()
            .WithMany(c => c.Lines)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
