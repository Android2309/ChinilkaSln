using Chinilka.Interfaces;
using Chinilka.Models;
using Chinilka.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chinilka.Pages
{
    public class CartModel : PageModel
    {
        private IChinilkaRepository repository;

        public CartModel(IChinilkaRepository repo, Cart cartService)
        {
            repository = repo;
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(long id, string returnUrl)
        {
            Product? product = repository.Products.FirstOrDefault(p => p.Id == id);

            if (product != null) 
            {
                Cart.AddItem(product, 1);
            }

            return RedirectToPage(new { returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.Id == productId).Product);
            return RedirectToPage(new { returnUrl });
        }
    }
}
