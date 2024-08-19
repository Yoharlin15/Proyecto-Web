using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Proyecto_Web.Constants;
using Proyecto_Web.Data;
using System.Net;

namespace Proyecto_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<NewsContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("NewsConnectionString"));
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<NewsContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("NewsConnectionString"));
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                 options.SlidingExpiration = true;
                 options.AccessDeniedPath = "/Forbidden/";
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
                pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

            app.Run();
        }
    }
}
