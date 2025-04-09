using BankApp.Data;
using BankApp.Repositories;
using BankApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodanie DbContext do kontenera DI (wstrzykiwanie zależności)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Dodanie pozostałych usług
builder.Services.AddControllersWithViews(); // Dodanie wsparcia dla MVC
builder.Services.AddScoped<ClientService>(); // Rejestracja serwisu do obsługi logiki biznesowej
builder.Services.AddScoped<IClientRepository, ClientRepository>(); // Rejestracja repozytorium dla Clienta

var app = builder.Build();

// Konfiguracja middleware
app.UseHttpsRedirection();
app.UseStaticFiles(); // Dodanie wsparcia dla plików statycznych (CSS, JS, obrazy itp.)
app.UseRouting();
app.UseAuthorization(); // Używanie autoryzacji

// Definicja routingu dla aplikacji
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

// Mapowanie kontrolerów, jeśli używasz API
app.MapControllers();

app.Run();