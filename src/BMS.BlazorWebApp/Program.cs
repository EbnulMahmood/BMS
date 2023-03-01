using BMS.BlazorWebApp.Data;
using BMS.Plugins.EFCore.Data;
using BMS.Plugins.EFCore.Extensions;
using BMS.UseCases.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BMSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BMSDbContext")));

builder.Services.AddBMSRepositories();
builder.Services.AddBMSServices();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
