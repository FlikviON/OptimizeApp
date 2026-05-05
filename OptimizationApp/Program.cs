using Microsoft.EntityFrameworkCore;
using OptimizationApp.Application.Interfaces;
using OptimizationApp.Application.Services;
using OptimizationApp.Domain.Interfaces;
using OptimizationApp.Endpoints;
using OptimizationApp.Infrastructure.Data;
using OptimizationApp.Infrastructure.Repositories;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

Batteries_V2.Init();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=optimization.db"));

builder.Services.AddScoped<IBiomeSettingRepository, BiomeSettingRepository>();
builder.Services.AddScoped<IBiomeSettingService, BiomeSettingService>();
builder.Services.AddScoped<IMapService, MapService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseStaticFiles();
app.MapGet("/", () => Results.Redirect("/index.html"));

app.MapMapEndpoints();
app.MapBiomeSettingEndpoints();

app.Run();
