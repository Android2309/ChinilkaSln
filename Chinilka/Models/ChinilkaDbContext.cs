using Chinilka.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinilka.Models
{
    public class ChinilkaDbContext : DbContext
    {
        public ChinilkaDbContext(DbContextOptions<ChinilkaDbContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<DeviceModel> DeviceModels => Set<DeviceModel>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<CartLine> CartLines => Set<CartLine>();

        // И Всё? Где индексы?
    }
}
