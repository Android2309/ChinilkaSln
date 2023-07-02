using System.ComponentModel.DataAnnotations;

namespace Chinilka.Infrastructure
{
    public class ImagePathValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value) => value != null && File.Exists(value.ToString());
    }
}
