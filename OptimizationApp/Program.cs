using Microsoft.EntityFrameworkCore;
using OptimizationApp;
using SQLitePCL;
using System;


var builder = WebApplication.CreateBuilder(args);

Batteries_V2.Init();

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=optimization.db"));

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
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/index.html"));


app.Run();
