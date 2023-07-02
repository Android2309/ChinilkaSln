using Chinilka.Models.Entities;

namespace Chinilka.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        public PagingInfo PagingInfo { get; set; } = new();
        public string? CurrentDeviceModel { get; set; }
    }
}
