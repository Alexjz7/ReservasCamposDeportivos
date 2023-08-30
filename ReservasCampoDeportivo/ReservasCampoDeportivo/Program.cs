using Microsoft.EntityFrameworkCore;
using ReservasCampoDeportivo.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure your DbContext here
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rest of the code remains the same
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Rest of the code remains the same
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
