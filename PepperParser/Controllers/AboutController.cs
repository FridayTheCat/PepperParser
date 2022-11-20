using Microsoft.AspNetCore.Mvc;
using PepperParser.Domain;
using PepperParser.Domain.Repositories.EF;

namespace PepperParser.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "О нас";
            ViewBag.Text = "Информация";
            return View();
        }
    }
}
