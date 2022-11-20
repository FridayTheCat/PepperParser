# PepperParser

В приложении реализованы щадящий автоматический парсинг данных с сайта Pepper.ru, админ-панель, отправка Email.
Дизайн - доработанный бесплатный шаблон HTML5 с сайта https://html5up.net

Проект развернут на бесплатном хостинге https://freeasphosting.net
Ссылка на релиз https://pepperparser.bsite.net

C# ASP NET CORE MVC Web приложение.

# Технологии

Использованы следующие библиотеки:  
· ASP NET CORE 6.0  
· EntityFrameworkCore 6.0.11  
· EntityFrameworkCore Tools 6.0.11  
· EntityFrameworkCore SqlServer 6.0.11  
· AspNetCore Identity EntityFrameworkCore 6.0.11  
· AspNetCore Identity UI 6.0.11  
· Hangfire AspNetCore 1.7.31  
· Hangfire SqlServer 1.7.31  
· MailKit 3.4.2  

# Инструкция по настройке

· В appsettings.json добавить строку подключения к вашей БД и по желанию изменить значение TimeCron, отвечающее за периодичность обновления данных о промокодах (парсинга)  

![image](https://user-images.githubusercontent.com/118601762/202902464-05e34ccf-9971-4447-a017-038a268501b9.png)

· В Domain/AppDbContext.cs изменить логин и пароль для доступа в админ-панель. Измеить Email (по желанию).  

![image](https://user-images.githubusercontent.com/118601762/202902895-53d1f4a5-40a6-4f68-879d-191c596862bb.png)

· В Services/Mail/Mail.cs изменить значения Email адресов отправителя и получателя письма, имени компании, а так же smtp сервер и порт от него вместе с данными для аутентификации  

![image](https://user-images.githubusercontent.com/118601762/202902554-496567cc-ddd3-491f-bbe7-9bce0e7208ad.png)

· Создать миграцию и применить ее к БД средствами EntityFrameworkCore  
Например в консоле NuGet пакетов:
1) Add-Migration Initial  
2) Update-Database  

· После запуска перейти к авторизации для доступа в админ панель  

![image](https://user-images.githubusercontent.com/118601762/202903865-ec8634db-9624-40ef-b318-b013374491b7.png)

· После авторизации создать фоновую задачу и войти в панель управления задачами  

![image](https://user-images.githubusercontent.com/118601762/202904024-725a4136-e701-48ec-a296-476b0ea538de.png)

· В панели управления перейти к повторябщимся задачам, выбрать задачу и запустить ее немедленно  

![image](https://user-images.githubusercontent.com/118601762/202904278-3ef1a1f9-0e99-438a-8d8d-a777a926b438.png)

· После чего вернуться в админ-панель и выйти из учетной записи администратора
