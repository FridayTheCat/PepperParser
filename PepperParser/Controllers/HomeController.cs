using Microsoft.AspNetCore.Mvc;

namespace PepperParser.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Pepper Parser";
            ViewBag.Text = "Главная";
            return View();
        }
    }
}