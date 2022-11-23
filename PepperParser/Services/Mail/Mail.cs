using MimeKit;
using MailKit.Net.Smtp;
using PepperParser.Domain.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace PepperParser.Services.Mail
{
    //Сервис отправки Email
    public static class Mail
    {
        public static int SendMail(Feedback feedback, [FromServices] IConfiguration config)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress(feedback.Name, config.GetValue<string>("MailFrom"))); //Откуда будет производиться отправка
                message.To.Add(new MailboxAddress(config.GetValue<string>("NameTo"), config.GetValue<string>("MailTo"))); //Куда будет производиться отправка
                message.Subject = $"Сообщение от {feedback.Email}";                      //Заголовок
                message.Body = new BodyBuilder() { TextBody = feedback.Message }.ToMessageBody(); //Сообщение

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect(config.GetValue<string>("Smtp"), config.GetValue<int>("SmtpPort"), config.GetValue<bool>("UseSSL"));    //Коннект к smtp серверу
                    client.Authenticate(config.GetValue<string>("MailFrom"), config.GetValue<string>("MailPass"));  //Данные для аутентификации
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