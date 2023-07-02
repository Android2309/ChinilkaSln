using Chinilka.Interfaces;
using Chinilka.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chinilka.Models
{
    public class EFChinilkaRepository : IChinilkaRepository
    {
        private ChinilkaDbContext context;
        public EFChinilkaRepository(ChinilkaDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products
            .Include(_ => _.DeviceModel);
        public IQueryable<Category> Categories => context.Categories;
        public IQueryable<DeviceModel> DeviceModels => context.DeviceModels
            .Include(_ => _.Category);
        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public async Task SaveOrder(Order order)
        {
            if (order.Id == 0)
            {
                await context.Orders.AddAsync(order);
            }

            await context.SaveChangesAsync();
        }
        public async Task CreateProduct(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }
        public async Task CreateProductsRange(IEnumerable<Product> products)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
        public async Task DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task CreateDeviceModel(DeviceModel deviceModel)
        {
            await context.DeviceModels.AddAsync(deviceModel);
            await context.SaveChangesAsync();
        }
        public async Task UpdateDeviceModel(DeviceModel deviceModel)
        {
            context.DeviceModels.Update(deviceModel);
            await context.SaveChangesAsync();
        }
        public async Task DeleteDeviceModel(DeviceModel deviceModel)
        {
            context.Remove(deviceModel);
            await context.SaveChangesAsync();
        }

        public async Task CreateCategory(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }
        public async Task UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }
        public async Task DeleteCategory(Category category)
        {
            context.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
