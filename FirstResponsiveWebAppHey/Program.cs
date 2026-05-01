using Microsoft.EntityFrameworkCore;
using FirstResponsiveWebAppHey.Models.Olympics;
using FirstResponsiveWebAppHey.Models.Ticketing;
using FirstResponsiveWebAppHey.Models.DataLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddDbContext<OlympicsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("OlympicsContext")));

builder.Services.AddDbContext<TicketingContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TicketingContext")));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "olympics",
    pattern: "Olympics/{action=Index}/game-{activeGame}/cat-{activeCat}",
    defaults: new { controller = "Home", action = "Index", activeGame = "all", activeCat = "all" });

app.MapControllerRoute(
    name: "custom-rule",
    pattern: "assignments/custom-rule",
    defaults: new { controller = "Assignments", action = "CustomRule" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();