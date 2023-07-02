using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chinilka.Models.Entities
{
    public class CartLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = new();
        public int Quantity { get; set; }
    }
}
