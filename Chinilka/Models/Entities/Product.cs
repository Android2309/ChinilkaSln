using Chinilka.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinilka.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public bool IsUsed { get; set; }

        [Required(ErrorMessage = "Необходимо название товара")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Необходимо описание")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть > 0")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Необходимо выбрать модель устрйоства")]
        public int DeviceModelId { get; set; }

        public DeviceModel? DeviceModel { get; set; }

        [ImagePathValidation(ErrorMessage ="Изображение по данному пути не существует")]
        public string? ImagePath { get; set; }
    }
}
