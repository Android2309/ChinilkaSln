using Chinilka.Interfaces;
using Chinilka.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Chinilka.Controllers
{
    public class HomeController : Controller
    {
        private IChinilkaRepository repository;

        public int PageSize = 4;
        public HomeController(IChinilkaRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index() => View();

        public async Task<ViewResult> DeviceModels(string? category)
        {
            return View(new DeviceModelsListViewModel
            {
                DeviceModels = await repository.DeviceModels
                    .Where(d => category == null || d.Category != null && d.Category.Name == category)
                    .OrderBy(p => p.Id).ToListAsync(),
                CurrentCategory = category
            });
        }

        public async Task<ViewResult> Products(string? deviceModel, int productPage = 1, bool isUsed = false)
        {
            return View(new ProductsListViewModel
            {
                Products = await repository.Products
                    .Where(p => (p.DeviceModel != null && p.DeviceModel.Name == deviceModel) && p.IsUsed == isUsed)
                    .OrderBy(p => p.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = await repository.Products.Where(_ => _.DeviceModel != null && _.DeviceModel.Name == deviceModel).CountAsync()
                },
                CurrentDeviceModel = deviceModel
            });
        }

        public async Task<ViewResult> SearchedProducts(string searchString, int productPage = 1)
        {
            return View(new SearchedProductsListViewModel
            {
                Products = await repository.Products
                    .Where(p => p.Name.Contains(searchString))
                    .OrderBy(p => p.Id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = await repository.Products.Where(p => p.Name.Contains(searchString)).CountAsync()
                },
                SearchString = searchString
            }); 
        }
    }
}
