using Chinilka.Models.Entities;

namespace Chinilka.Interfaces
{
    // Почему выбор пал на паттерн репозитория? Это не плохо, мне интересны причины.
    public interface IChinilkaRepository
    {
        IQueryable<Product> Products { get; }

        IQueryable<Category> Categories { get; }

        IQueryable<DeviceModel> DeviceModels { get; }

        IQueryable<Order> Orders { get; }
        Task SaveOrder(Order order);

        Task CreateProduct(Product product);
        Task CreateProductsRange(IEnumerable<Product> products);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);

        Task CreateDeviceModel(DeviceModel deviceModel);
        Task UpdateDeviceModel(DeviceModel deviceModel);
        Task DeleteDeviceModel(DeviceModel deviceModel);

        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
    }
}
