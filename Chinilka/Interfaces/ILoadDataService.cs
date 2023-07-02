using Microsoft.AspNetCore.Components.Forms;

namespace Chinilka.Interfaces
{
    public interface ILoadDataService
    {
        Task LoadFromFileAsync(string path);
    }
}
