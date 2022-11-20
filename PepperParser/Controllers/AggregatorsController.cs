using Microsoft.AspNetCore.Mvc;

namespace PepperParser.Controllers
{
    public class AggregatorsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Площадки";
            ViewBag.Text = "Актуальные промокоды";
            return View();
        }
    }
}
