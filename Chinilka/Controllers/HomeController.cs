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

        // private const? Кстати, почему фронт не передаёт размер страницы?
        public int PageSize = 4;

        public HomeController(IChinilkaRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index() => View();

        // Категория звучит как енум. Сейчас выходит, что фронт может сюда любые данные передать.
        public async Task<ViewResult> DeviceModels(string? category)
        {
            return View(new DeviceModelsListViewModel
            {
                DeviceModels = await repository.DeviceModels
                    // а если пустую строку передам?
                    .Where(d => category == null || d.Category != null && d.Category.Name == category)
                    .OrderBy(p => p.Id)
                    // Зачем асинхронность?
                    .ToListAsync(),
                CurrentCategory = category
            });
        }

        // Ну то есть у нас фильтр возможен только по названию девайса (причём по точному совпадению!) и по б/у. А если я дальше по бренду захочу фильтровать? По цене?
        // Хотя с другой стороны тут можно очень глубоко копнуть. Ведь для разных устройств нужны разные характеристики. Так можно слишком всё усложнить,
        // и сделать аля у каждого продукта список свойств словариком, и по этому словаику потом фильтровать. На старте жизни сервиса такое всё же избыточно.
        // Короче, твой вариант кажется слишком простым, мой - слишком сложным. Нужно что-то усреднённое.
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
