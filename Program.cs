using CrudCore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Inyectar el DbContext con la cadena de conexión
builder.Services.AddDbContext<DbcrudcoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql")));

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
