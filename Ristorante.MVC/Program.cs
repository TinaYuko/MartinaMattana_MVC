using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Ristorante.Core.BL;
using Ristorante.Core.Interfaces;
using Ristorante.EF;
using Ristorante.EF.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IBusinessLayer, BusinessLayer>();

builder.Services.AddScoped<IMenuRepository, MenuRepositoryEF>();
builder.Services.AddScoped<IPiattoRepository, PiattoRepositoryEF>();
builder.Services.AddScoped<IUtenteRepository, UtenteRepositoryEF>();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Ristorante;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option =>
    {
        option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Utente/Login");
        option.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Utente/Forbidden");
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Adm", policy => policy.RequireRole("Administrator"));
    options.AddPolicy("Cl", policy => policy.RequireRole("Cliente"));
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
