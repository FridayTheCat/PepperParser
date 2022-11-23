using Microsoft.AspNetCore.Mvc;
using PepperParser.Domain.Repositories.EF;
using PepperParser.Domain;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using PepperParser.Services.Parser;

namespace PepperParser.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HangfireController : Controller
    {
        private readonly EFPromocodeRepository eFPromocode;
        private readonly AppDbContext _context;

        //Список ссылок для парсинга
        private readonly List<string> urls = new List<string>()
        {
            "https://www.pepper.ru/coupons/samokat.ru",
            "https://www.pepper.ru/coupons/sbermarket.ru",
            "https://www.pepper.ru/coupons/eda.yandex",
            "https://www.pepper.ru/coupons/delivery-club.ru",
            "https://www.pepper.ru/coupons/ozon.ru",
            "https://www.pepper.ru/coupons/kazanexpress.ru",
            "https://www.pepper.ru/coupons/wildberries.ru",
            "https://www.pepper.ru/coupons/dodopizza.ru",
            "https://www.pepper.ru/coupons/shop.lenta.com",
            "https://www.pepper.ru/coupons/papajohns.ru",
            "https://www.pepper.ru/coupons/lavka.yandex",
            "https://www.pepper.ru/coupons/kfc.ru",
            "https://www.pepper.ru/coupons/aliexpress.com"
        };

        public HangfireController(AppDbContext context, IConfiguration config)
        {
            eFPromocode = new EFPromocodeRepository(context, config);
            _context = context;
        }

        //Метод создающий повторяющуюся фоновую задачу
        public IActionResult UpdateDb([FromServices] IConfiguration config)
        {
            //Получение значения Cron из конфига
            var cron = config.GetValue<string>("TimeCron");
            //Установка флага первого запуска
            GetRequest.IsFirstStart = true;
            //Создание задачи
            RecurringJob.AddOrUpdate(() => eFPromocode.UpdateAllPromocode(urls), cron);

            TempData["JobAdded"] = "Ok";
            return RedirectToAction("Index", "Hangfire");
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Админ-панель";
            ViewBag.Text = "Панель Администратора";
            return View();
        }

        //Показ всех промокодов из БД
        public IActionResult ListOfPromocodes()
        {
            ViewBag.Title = "Все промокоды";
            ViewBag.Text = "Список промокодов из БД";

            //Получение промокодов из БД
            var listCodes = _context.Promocode.ToList();
            return View(listCodes);
        }

        //Удаление промокода из БД по ID
        [HttpPost]
        public IActionResult DeletePromo(int id)
        {
            _context.Promocode.Remove(_context.Promocode.FirstOrDefault(p => p.Id == id));
            _context.SaveChanges();
            return RedirectToAction("ListOfPromocodes", "Hangfire");
        }

        //Удаление всех промокодов из БД
        [HttpPost]
        public IActionResult DeleteAllPromo()
        {
            _context.Promocode.RemoveRange(_context.Promocode);
            //Сброс PrimaryKey
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Promocode', RESEED, 0)");

            _context.SaveChanges();
            return RedirectToAction("ListOfPromocodes", "Hangfire");
        }
    }
}
