using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;

namespace WebApplication5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            
            
            
            builder.Services.AddDbContext<AppDbContext>(opt =>
         opt.UseSqlServer("server=.;database=Guler12;Trusted_Connection=True;Encrypt=False")
         );
            
            
            
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._@+";
                opt.User.RequireUniqueEmail = true;
                //opt.Password.RequireNonAlphanumeric = true;
                //opt.Password.RequiredLength = 6;
                //opt.Lockout.AllowedForNewUsers = true;
                //opt.Lockout.MaxFailedAccessAttempts = 3;
                //opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            }).AddEntityFrameworkStores<AppDbContext>();

            
            var app = builder.Build();

            app.UseStaticFiles();

            app.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"

           );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}"
                );

            app.Run();
        }
    }
}

