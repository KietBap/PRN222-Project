using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using PRN222.RoomBooking.AdminBlazor.Auth;
using PRN222.RoomBooking.AdminBlazor.Data;
using PRN222.RoomBooking.AdminBlazor.Hubs;
using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.UnitOfWork;
using PRN222.RoomBooking.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<FpturoomBookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));

// Add scoped services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICampusService, CampusService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();


// Add AuthenticationStateProvider
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();

// Add Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ProtectedLocalStorage>();

// Add Blazor services
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapHub<NotificationHub>("/Hub");

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();