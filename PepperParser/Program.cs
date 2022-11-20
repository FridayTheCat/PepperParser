using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PepperParser.Domain;
using PepperParser.Domain.Repositories.Abstract;
using PepperParser.Domain.Repositories.EF;
using PepperParser.Filters;
using PepperParser.Services.AdminAreaAuthorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IPromocodeRepository, EFPromocodeRepository>();

builder.Services.AddDbContext<AppDbContext>(x => 
        x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Фоновые задачи
builder.Services.AddHangfire(h => 
        h.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

//Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

//Куки
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "PepperParserAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
});

//Добавление политики авторизации
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
});

//MVC
builder.Services.AddControllersWithViews(x =>
{
    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
})
    .AddSessionStateTempDataProvider();

//Сессии
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/dashboard", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Hangfire}/{action=Index}/{id?}");
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
