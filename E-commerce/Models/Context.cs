using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Models
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions<Context> options)
      : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18,2)");
            });

        }
    }
}