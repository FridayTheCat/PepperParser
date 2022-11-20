using Microsoft.AspNetCore.Mvc;

namespace PepperParser.Controllers
{
    public class DonationController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Донат";
            ViewBag.Text = "Помощь сайту";
            return View();
        }
    }
}
