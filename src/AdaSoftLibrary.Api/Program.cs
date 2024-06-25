using AdaSoftLibrary.Application;
using AdaSoftLibrary.Application.Common.Configurations;
using AdaSoftLibrary.Application.Extensions;
using AdaSoftLibrary.Infrastructure;
using Carter;
using Microsoft.AspNetCore.Http.Json;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Application configuration
builder.Services.ConfigureOptions<ApplicationOptionsSetup>();

// JSON serialization
builder.Services.Configure<JsonOptions>(options => new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true });

// Logging
builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

// Add services to the container
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

// Global exception handling
builder.Services.AddGlobalErrorHandler();

// Add services to the container.
builder.Services.AddSwagger();
builder.Services.AddCarter();

var app = builder.Build();
app.Logger.LogInformation($"AdaSoftLibrary.API starting...");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add Middlewares
app.UseGlobalErrorHandlerMiddleware();

// Map API 
app.MapCarter();

app.Run();
