using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PRN222.RoomBooking.Repositories;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.UnitOfWork;
using PRN222.RoomBooking.Services;

namespace PRN222.RoomBooking.ManagerMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSignalR();

            builder.Services.AddDbContext<FpturoomBookingDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<ICampusService, CampusService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "ManagerAuthCookie"; // Đặt tên cookie riêng cho Manager
                    options.LoginPath = "/User/Login";
                    options.LogoutPath = "/User/Logout";
                    options.AccessDeniedPath = "/Home/AccessDenied";
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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
