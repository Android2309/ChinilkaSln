using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// Зачем?
#nullable disable

namespace Chinilka.Models.ViewModels
{
    public class RegisterModel
    {
        // Что с локализацией делать планируешь? В Program.cs локализация упоминалась.

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Как Вас зовут?")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
    }
}
