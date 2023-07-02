using Chinilka.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Chinilka.Models.Entities
{
    public class DeviceModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо название модели")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category? Category { get; set; }

        [ImagePathValidation(ErrorMessage = "Изображение по данному пути не существует")]
        public string? ImagePath { get; set; }
    }
}
