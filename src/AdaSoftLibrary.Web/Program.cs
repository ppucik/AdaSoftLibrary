using AdaSoftLibrary.Application;
using AdaSoftLibrary.Application.Common.Configurations;
using AdaSoftLibrary.Application.Extensions;
using AdaSoftLibrary.Infrastructure;
using AdaSoftLibrary.Infrastructure.Extensions;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Application configuration
builder.Services.ConfigureOptions<ApplicationOptionsSetup>();

// Logging
builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

// Add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Access/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
    });

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddNotyf((config) =>
{
    config.DurationInSeconds = 3;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

// Global exception handling
builder.Services.AddGlobalErrorHandler();

var app = builder.Build();

app.Logger.LogInformation($"AdaSoftLibrary.Web starting...");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Security
app.UseAuthentication();
app.UseAuthorization();

// Add Middlewares
app.UseGlobalErrorHandlerMiddleware();

// Map API 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
