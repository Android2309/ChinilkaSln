using Chinilka.Models.Entities;

namespace Chinilka.Models.ViewModels
{
    public class DeviceModelsListViewModel
    {
        public IEnumerable<DeviceModel> DeviceModels { get; set; } = Enumerable.Empty<DeviceModel>();
        public string? CurrentCategory { get; set; }
    }
}
