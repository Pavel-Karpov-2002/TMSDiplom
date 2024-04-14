using Diploma.Controllers;
using Diploma.Models.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Diploma.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Ввод логина обязателен!")]
        [MaxLength(16, ErrorMessage = "Логин не должен быть длинее 16 символов")]
        [MinLength(2, ErrorMessage = "Логин не должен быть меньше 2 символов!")]
        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "Логин должен состоять только из латинских букв и цифр!")]
        [Remote(action: nameof(RegistrationController.VerifyLogin), controller: "Registration", ErrorMessage = "Логин уже используется")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Ввод почты обязателен!")]
        [EmailAddress(ErrorMessage = "Некорректно введена почта! Пример: pochta@yandex.by")]
        [Remote(action: nameof(RegistrationController.VerifyEmail), controller: "Registration", ErrorMessage = "Почта уже используется")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ввод пароля обязателен!")]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля 6 символов!")]
        [IsLetterUppercase(1, ErrorMessage = "Пароль должен иметь минимум 1 букву верхнего регистра!")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Ввод имени обязателен!")]
        public string Username { get; set; }
    }
}
