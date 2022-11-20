using Microsoft.AspNetCore.Mvc;
using PepperParser.Domain;
using PepperParser.Domain.Repositories.EF;
using PepperParser.Services.Parser;

namespace PepperParser.Controllers
{
    public class PromocodesController : Controller
    {
        private readonly EFPromocodeRepository eFPromocode;
        public PromocodesController(AppDbContext context)
        {
            eFPromocode = new EFPromocodeRepository(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Samokat()
        {
            ViewBag.Title = "Самокат";
            ViewBag.Text = "Промокоды для Самокат";
            
            var promocodes = eFPromocode.GetPromocodeByAgregatorName("samokat.ru");
            
            return View("Index", promocodes);
        }

        public IActionResult Sbermarket()
        {
            ViewBag.Title = "Сбермаркет";
            ViewBag.Text = "Промокоды для Сбермаркет";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("sbermarket.ru");

            return View("Index", promocodes);
        }

        public IActionResult YandexEda()
        {
            ViewBag.Title = "Яндекс.Еда";
            ViewBag.Text = "Промокоды для Яндекс.Еда";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("eda.yandex");

            return View("Index", promocodes);
        }

        public IActionResult DeliveryClub()
        {
            ViewBag.Title = "Delivery Club";
            ViewBag.Text = "Промокоды для Delivery Club";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("delivery-club.ru");

            return View("Index", promocodes);
        }

        public IActionResult Ozon()
        {
            ViewBag.Title = "Ozon";
            ViewBag.Text = "Промокоды для Ozon";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("ozon.ru");

            return View("Index", promocodes);
        }

        public IActionResult KazanExpress()
        {
            ViewBag.Title = "KazanExpress";
            ViewBag.Text = "Промокоды для KazanExpress";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("kazanexpress.ru");

            return View("Index", promocodes);
        }

        public IActionResult Wildberries()
        {
            ViewBag.Title = "Wildberries";
            ViewBag.Text = "Промокоды для Wildberries";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("wildberries.ru");

            return View("Index", promocodes);
        }

        public IActionResult Dodo()
        {
            ViewBag.Title = "Додо Пицца";
            ViewBag.Text = "Промокоды для Додо Пиццы";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("dodopizza.ru");

            return View("Index", promocodes);
        }

        public IActionResult Lenta()
        {
            ViewBag.Title = "Лента Онлайн";
            ViewBag.Text = "Промокоды для Лента Онлайн";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("shop.lenta.com");

            return View("Index", promocodes);
        }

        public IActionResult PapaJons()
        {
            ViewBag.Title = "Папа Джонс";
            ViewBag.Text = "Промокоды для Папа Джонс";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("papajohns.ru");

            return View("Index", promocodes);
        }

        public IActionResult YandexLavka()
        {
            ViewBag.Title = "Яндекс.Лавка";
            ViewBag.Text = "Промокоды для Яндекс.Лавка";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("lavka.yandex");

            return View("Index", promocodes);
        }

        public IActionResult KFC()
        {
            ViewBag.Title = "KFC";
            ViewBag.Text = "Промокоды для KFC";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("kfc.ru");

            return View("Index", promocodes);
        }

        public IActionResult Aliexpress()
        {
            ViewBag.Title = "AliExpress";
            ViewBag.Text = "Промокоды для AliExpress";

            var promocodes = eFPromocode.GetPromocodeByAgregatorName("aliexpress.com");

            return View("Index", promocodes);
        }
    }
}
