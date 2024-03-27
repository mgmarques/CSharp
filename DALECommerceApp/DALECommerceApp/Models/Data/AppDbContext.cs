using Microsoft.EntityFrameworkCore;

namespace DALECommerceApp.Models.Data
{
	public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public string DbPath { get; }

        public AppDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "eCommerceApp.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath};Cache=Shared");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
              .HasMany(d => d.Orders)
              .WithOne(s => s.Customer)
              .HasForeignKey(s => s.CustomerId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
              .HasMany(d => d.OrderItems)
              .WithOne(s => s.Order)
              .HasForeignKey(s => s.OrderId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
              .HasMany(p => p.OrderItems)
              .WithOne(s => s.Product)
              .HasForeignKey(s => s.ProductId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
              .HasIndex(u => u.Email)
              .IsUnique();

            modelBuilder.Entity<Order>()
              .HasIndex(u => u.Status);

            modelBuilder.Entity<Order>()
              .HasIndex(u => u.SaleStatus);

            modelBuilder.Entity<Product>()
              .HasIndex(u => u.Category);

            modelBuilder.Entity<OrderItem>()
             .HasKey(c => new { c.OrderId, c.ProductId });
        }
    }
}
