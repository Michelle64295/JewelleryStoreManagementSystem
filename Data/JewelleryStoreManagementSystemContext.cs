using JewelleryStoreManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace JewelleryStoreManagementSystem.Data
{
    public class JewelleryStoreManagementSystemContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public string DbPath { get; }

        public JewelleryStoreManagementSystemContext(DbContextOptions<JewelleryStoreManagementSystemContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "JewelleryStore.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={DbPath}");
            }
        }
    }
}
