using AdaSoftLibrary.Application;
using AdaSoftLibrary.Application.Common.Configurations;
using AdaSoftLibrary.Application.Extensions;
using AdaSoftLibrary.Infrastructure;
using Carter;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Application configuration
builder.Services.ConfigureOptions<ApplicationOptionsSetup>();

// JSON serialization
builder.Services.Configure<JsonOptions>(options => new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true });

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
