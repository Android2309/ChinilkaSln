using Chinilka.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chinilka.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IChinilkaRepository repository;

        public NavigationMenuViewComponent(IChinilkaRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(repository.Categories.Select(x => x.Name));
        }
    }
}
