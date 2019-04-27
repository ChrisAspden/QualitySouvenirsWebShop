using QualitySouvenirs.Models;
using Microsoft.EntityFrameworkCore;

namespace QualitySouvenirs.Data
{
    public class SouvenirContext : DbContext
    {
        public SouvenirContext(DbContextOptions<SouvenirContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Souvenir> Souvenirs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<Souvenir>().ToTable("Souvenir");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
        }
    }
}
