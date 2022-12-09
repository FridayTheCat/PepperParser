using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PepperParser.Domain;
using PepperParser.Domain.Implementation;
using System.Net;
using System.Runtime.InteropServices;

namespace PepperParser.Services.Parser
{
    //Парсер промокодов с Pepper
    public static class GetRequest
    {
        //Прокси для отслеживания пакетов через Fiddler, уже не нужны
        //private static WebProxy proxy = new WebProxy("localhost", 8888);

        private static CookieContainer cookieContainerForList;
        private static CookieContainer cookieContainerForCodes;
        public static bool IsFirstStart;

        //Парсинг основной информации о промокодах
        public static List<Promocode> ParseIDs(AppDbContext _context, string url)
        {
            var listOfPromocode = new List<Promocode>();
            var listOfCodes = new List<string>();
            cookieContainerForList = new CookieContainer();

            using (HttpClientHandler hdl = new HttpClientHandler { CookieContainer = cookieContainerForList, /*Proxy = proxy,*/ })
                {
                    using (var clnt = new HttpClient(hdl))
                    {
                        Uri uri = new Uri(url);
                        cookieContainerForList.Add(uri, new Cookie("lcl", "%22309636%22"));
                        cookieContainerForList.Add(uri, new Cookie("hide_expired", "%221%22"));

                        using (HttpResponseMessage resp = clnt.GetAsync(url).Result)
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                var html = resp.Content.ReadAsStringAsync().Result;

                                if (!string.IsNullOrEmpty(html))
                                {
                                      int nameStart = 1;
                                      int nameEnd = 1;
                                      string name = "";

                                      int idStart =1;
                                      int idEnd = 1;
                                      string id = "";

                                      string part = "";
                                      int lengthOfPart = 850;
                                      int avalibleToStart = 1;
                                      int avalibleToEnd = 1;
                                      string avalibleTo = "";

                                      string code = "";

                                      string agregator = "";

                                      //Выборка имен всех промокодов
                                      var DbNameList = _context.Promocode.Select(p => p.Name).ToList();

                                    while (true)
                                    {
                                        //Поиск имени
                                        nameStart = html.IndexOf("merchant-v2\">", nameEnd);

                                        if (nameStart == -1) break;

                                        part = html.Substring(nameStart, lengthOfPart);

                                        nameStart += 13;
                                        nameEnd = html.IndexOf("</span", nameStart);
                                        name = html.Substring(nameStart, nameEnd - nameStart).Trim();

                                        if (name.Contains("&quot;"))
                                            name = FixQuot(name);

                                        //Поиск ID
                                        idStart = html.IndexOf("threadvmpf/", nameEnd);
                                        idStart += 11;
                                        idEnd = html.IndexOf("\"", idStart);
                                        id = html.Substring(idStart, idEnd - idStart).Trim();
                                        
                                        //Поиск срока действия
                                        avalibleToStart = part.IndexOf("Истекает");
                                        if (avalibleToStart == -1)
                                        {
                                            avalibleTo = "Неизвестно";
                                        }
                                        else
                                        {
                                            avalibleToStart += 31;
                                            avalibleToEnd = part.IndexOf("</span>", avalibleToStart);
                                            //Исправление положения для некоторых промокодов (у них немного отличается html код)
                                            if (avalibleToEnd == -1 || (avalibleToEnd-avalibleToStart) > 20)
                                            {
                                                avalibleToStart -= 22;
                                                avalibleToEnd = part.IndexOf("</span>", avalibleToStart);
                                            }
                                            avalibleTo = part.Substring(avalibleToStart, avalibleToEnd - avalibleToStart).Trim();
                                        }
                                        //Проверка на парсинг уже имеющегося в БД промокода
                                        if (DbNameList.Contains(name))
                                        {
                                            var oldPromo = _context.Promocode.Where(p => p.Name == name && p.IdOnPepper == id).ToListAsync().Result.FirstOrDefault();

                                            if (oldPromo == null)
                                                continue;

                                            //Обновление имеющегося промокода и установка ему флага Continued
                                            oldPromo.AvalibleTo = avalibleTo;
                                            oldPromo.Continued = true;
                                            _context.Promocode.Update(oldPromo);
                                            
                                            continue;
                                        }
                                    //Получение названия агрегатора/площадки из url
                                    agregator = url.Substring(30);

                                    //При повторных парсингах передаем контекст
                                    if (!IsFirstStart)
                                    {
                                        code = ParsePromo(url, id, _context);
                                    }
                                    else
                                    {
                                        //При первом запуске БД пуста, передаем лист с кодами, которые будут пополняться на каждой итерации
                                        code = ParsePromo(url, id, _context, listOfCodes);
                                        listOfCodes.Add(code);
                                    }
                                        //Добавление в основной List данных о каждом новом промокоде
                                        listOfPromocode.Add(new Promocode() { IdOnPepper = id, Name = name, AvalibleTo = avalibleTo, Agregator = agregator, Code = code, Continued = true});
                                    }

                                    _context.SaveChanges();

                                    return listOfPromocode;
                                }
                            }
                        }
                    }
                }

            return null;
        }

        //Парсинг самого промокода
        [JSInvokable]
        private static string ParsePromo(string url, string id, AppDbContext _context, [Optional] List<string> allKnownCodes)
        {
            string code = "";
            int codeStart = 1;
            int codeEnd = 1;
            List<string> listOfCodes;

            //При повторном запуске получаем имеющиеся промокоды из БД
            if (!IsFirstStart)
            {
                listOfCodes = _context.Promocode.Where(p => p.Agregator == url.Substring(30)).Select(p => p.Code).ToList();
            }
            else
            {
                //При первом БД пуста, получаем новый список при каждом вызове
                listOfCodes = allKnownCodes;
            }

            cookieContainerForCodes = new CookieContainer();
            using (HttpClientHandler hdl = new HttpClientHandler { CookieContainer = cookieContainerForCodes })
            {
                using (HttpClient clnt = new HttpClient(hdl))
                {
                    Uri uri = new Uri(url);
                    cookieContainerForCodes.Add(uri, new Cookie("show_voucher", id));
                    using (HttpResponseMessage resp = clnt.GetAsync(url).Result)
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var html = resp.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(html))
                            {
                                //Поиск промокода избегая повторов
                                while (true)
                                {
                                    codeStart = html.IndexOf("mini clickable\"", codeEnd);

                                    //При неудачи возвращаем "Неизвестно"
                                    if (codeStart == -1)
                                    {
                                        return "Неизвестно";
                                    }

                                    codeStart += 26;
                                    codeEnd = html.IndexOf("\"", codeStart);
                                    code = html.Substring(codeStart, codeEnd - codeStart).Trim();

                                    //Если промокод уникальный - возвращаем его
                                    if (!listOfCodes.Contains(code))
                                    {
                                        return code;
                                    }
                                }
                            }
                        }
                        return null;
                    }
                }
            }
        }

        private static string FixQuot(string nameWithProblem) => nameWithProblem.Replace("&quot;", "\"");
    }
}