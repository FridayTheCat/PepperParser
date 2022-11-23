using Microsoft.AspNetCore.Mvc;
using PepperParser.Domain.Implementation;
using PepperParser.Services.Mail;

namespace PepperParser.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendFeedback(Feedback feedback, [FromServices] IConfiguration config)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", feedback);
            }

            //Отправка сообщения
            var resposne = Mail.SendMail(feedback, config);

            if (resposne == StatusCodes.Status200OK)
            {
                TempData["ShowSuccses"] = "Ok";
                return RedirectToAction("Index", "Home");
            }

            if (resposne == StatusCodes.Status400BadRequest)
            {
                TempData["ShowSuccses"] = "Bad";
            }

            return View("Index", feedback);
        }
    }
}
