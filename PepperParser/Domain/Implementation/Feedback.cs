using PepperParser.Domain.Interface;
using System.ComponentModel.DataAnnotations;

namespace PepperParser.Domain.Implementation
{
    //Модель формы для обратрной связи
    public class Feedback : IFeedback
    {
        [Display(Name = "Имя:")]
        [Required(ErrorMessage = "Необходимо ввести Имя")]
        public string Name { get ; set; }

        [Required(ErrorMessage = "Необходимо ввести Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email:")]
        public string Email { get ; set ; }

        [Required(ErrorMessage = "Нельзя отправить пустое сообщение")]
        [Display(Name = "Сообщение:")]
        public string Message { get; set; }
    }
}
