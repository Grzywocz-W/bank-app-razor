using System.Globalization;
using BankApp.Data;
using BankApp.Repositories;
using BankApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja repozytoriów
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<AccountRepository>();

// Rejestracja serwisów
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<AccountService>();

// Rejestracja sesji
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

// Rejestracja baz danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Rejestracja kontrolerów
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Definiowanie domyślnej trasy, która będzie przekierowywać na stronę logowania
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Domyślnie akcja Login w kontrolerze Client

// app.UseEndpoints(endpoints =>
// {
//     // Zmieniamy domyślny kontroler na Home, a akcję na Index
//     endpoints.MapControllerRoute(
//         name: "default",
//         pattern: "{controller=Home}/{action=Index}/{id?}");
// });

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.Run("http://localhost:5062");  // Ustawienie portu, na którym aplikacja będzie uruchomiona