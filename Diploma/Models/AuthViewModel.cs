using Diploma.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Diploma.Models
{
    public class AuthViewModel
    {
        [Required(ErrorMessage = "Введите логин!")]
        [Remote(action: nameof(RegistrationController.VerifyLoginOrEmailAuthorization), controller: "Registration", ErrorMessage = "Логин или почта не существуют")]
        [MaxLength(16, ErrorMessage = "Максимальная длина логина 16 символов!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль!")]
        public string Password { get; set; }
    }
}
