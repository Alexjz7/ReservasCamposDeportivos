using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ReservasCampoDeportivo.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure your DbContext here
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rest of the code remains the same
builder.Services.AddControllersWithViews();
//Agregar autenticación por Cookies-->permisos
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Accesso/Login";
        option.AccessDeniedPath = "/Home/Privacy";

    });
var app = builder.Build();

// Rest of the code remains the same
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
