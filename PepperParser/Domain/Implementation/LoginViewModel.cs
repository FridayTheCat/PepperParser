using System.ComponentModel.DataAnnotations;

namespace PepperParser.Domain.Implementation
{
    //Модель формы для авторизации
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [UIHint("password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
