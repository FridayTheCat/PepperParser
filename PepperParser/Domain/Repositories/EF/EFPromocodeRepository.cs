using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PepperParser.Domain.Implementation;
using PepperParser.Domain.Repositories.Abstract;
using PepperParser.Services.Mail;
using PepperParser.Services.Parser;

namespace PepperParser.Domain.Repositories.EF
{
    public class EFPromocodeRepository : IPromocodeRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public EFPromocodeRepository(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //Получение из БД промокодов по агрегатору/площадке
        public List<Promocode> GetPromocodeByAgregatorName(string agregatorName) =>
            _context.Promocode.Where(x => x.Agregator == agregatorName).ToList();

        //Фоновая задача обновления данных обо всех промокодах
        public void UpdateAllPromocode(List<string> urls)
        {
            //Парсинг данных по всем ссылкам и сохранения их в БД
            foreach (var url in urls)
            {
                var promocodes = GetRequest.ParseIDs(_context, url);
                _context.Promocode.AddRange(promocodes);
            }

            //Удаление всех промокодов, которых больше нет на Peppper
            _context.Promocode.RemoveRange(_context.Promocode.Where(p => p.Continued == false));
            _context.SaveChanges();

            //Снятие флага первого запуска
            GetRequest.IsFirstStart = false;

            //Сортировка и выгрузка всех оставшихся промокодов из БД
            var AllPromo = _context.Promocode.OrderBy(p => p.Agregator).ToList();

            List<Promocode> newListPromo = new List<Promocode>();

            //Перенос всех промокодов в новый List с снятием флага Continued
            foreach (var promocode in AllPromo)
            {
                newListPromo.Add(new Promocode() { IdOnPepper = promocode.IdOnPepper, Name = promocode.Name, AvalibleTo = promocode.AvalibleTo, Agregator = promocode.Agregator, Code = promocode.Code, Continued = false });
            }

            //Очистка БД и сброс PrimaryKey
            _context.Promocode.RemoveRange(_context.Promocode);
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Promocode', RESEED, 0)");

            //Добавления всех оставшихся промокодов в БД
            _context.Promocode.AddRange(newListPromo);
            _context.SaveChanges();
            
            //Отправка Email оповещения об успешном обновлении БД
            Mail.SendMail(new Feedback() { Email = "AdminPepper@pepper.com", Name = "Hangfire", Message = "База данных успешно обновлена!" }, _config);
        }
    }
}
