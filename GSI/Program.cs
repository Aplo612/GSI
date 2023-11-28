using Microsoft.EntityFrameworkCore;
using GSI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventarioproyectogsiContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("inventarioproyectogsi"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("inventarioproyectogsi"))));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.LoginPath = "/Home/Login"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
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
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
