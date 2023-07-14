using Chinilka.Interfaces;
using Chinilka.Models;
using Chinilka.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chinilka.Controllers
{
    public class OrderController : Controller
    {
        private IChinilkaRepository repository;

        // Это зачем в DI?
        private Cart cart;
        public OrderController(IChinilkaRepository repoService
            // Так всё же сервис, или корзина?
            , Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        [Authorize]
        public ViewResult Checkout()
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваш заказ пуст!");
            }

            return View(new Order()
            {
                Lines = cart.Lines,
                Name = User.Identity?.Name
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines;

                //TODO: иначе ошибка
                //InvalidOperationException: The instance of entity type 'DeviceModel' cannot be tracked because another
                //instance with the key value '{Id: 35}' is already being tracked. When attaching existing entities, ensure
                //that only one entity instance with a given key value is attached.
                foreach (var line in order.Lines)
                {
                    line.Product = null;
                }

                await repository.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.Id });
            }
            else
            {
                return View();
            }
        }
    }
}
