using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using net8Proyect.Data;
using net8Proyect.Data.Data.Repository;
using net8Proyect.Data.Data.Repository.IRepository;
using net8Proyect.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//conexion a la base de datos

var connectionString = builder.Configuration.GetConnectionString("ConexionSQL") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//confirmacon de autenticacion con identity, normalmente esta en true pero lo cambie a false porque no lo necesitamos por el momento
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();
builder.Services.AddControllersWithViews();

//agregar el contenedor de trabajo

builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
