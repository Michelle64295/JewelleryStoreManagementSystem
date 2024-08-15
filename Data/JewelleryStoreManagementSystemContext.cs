using JewelleryStoreManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace JewelleryStoreManagementSystem.Data
{
    public class JewelleryStoreManagementSystemContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public string DbPath { get; } = null!;

        public JewelleryStoreManagementSystemContext()
        {
            
        }
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
