using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Chinilka.Models.Entities
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }

        [BindNever]
        public int UserId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

        [Required(ErrorMessage = "Пожалуйста введите Ваше имя")]
        public string? Name { get; set; }

        [BindNever]
        public bool Shipped { get; set; }
    }
}
