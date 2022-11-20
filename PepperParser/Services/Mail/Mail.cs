using MimeKit;
using MailKit.Net.Smtp;
using PepperParser.Domain.Implementation;

namespace PepperParser.Services.Mail
{
    //Сервис отправки Email
    public static class Mail
    {
        public static int SendMail(Feedback feedback)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress(feedback.Name, "YourEmail@Email.Com")); //Откуда будет производиться отправка
                message.To.Add(new MailboxAddress("CompanyName", "CompanyEmail@Eail.Com")); //Куда будет производиться отправка
                message.Subject = $"Сообщение от {feedback.Email}";                      //Заголовок
                message.Body = new BodyBuilder() { TextBody = feedback.Message }.ToMessageBody(); //Сообщение

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.mail.ru", 465, true);    //Коннект к smtp серверу
                    client.Authenticate("YourEmail@Email.Com", "YourPass");  //Данные для аутентификации
                    client.Send(message);                       //Отправка
                    client.Disconnect(true);                    //Дисконнект
                }
                return StatusCodes.Status200OK;
            }
            catch
            {
                return StatusCodes.Status400BadRequest;
            }
        }
    }
}