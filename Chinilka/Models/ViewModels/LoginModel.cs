using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Chinilka.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        public string? Password { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }
    }
}
