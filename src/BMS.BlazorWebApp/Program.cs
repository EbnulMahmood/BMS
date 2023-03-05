using BMS.BlazorWebApp.Data;
using BMS.Plugins.EFCore.Data;
using BMS.Plugins.EFCore.Extensions;
using BMS.UseCases.Extensions;
using Microsoft.EntityFrameworkCore;
using BMS.CoreBusiness.Entities;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using BMS.BlazorWebApp.Securities;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();
builder.Services.AddScoped<TokenProvider>();

builder.Services.AddDbContext<BMSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BMSDbContext"), b => b.MigrationsAssembly("BMS.BlazorWebApp")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BMSDbContext>();

builder.Services.AddBMSRepositories();
builder.Services.AddBMSServices();

builder.Services.Configure<OpenIdConnectOptions>(
    OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.SaveTokens = true;

        options.Scope.Add("offline_access");
    });

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

app.MapBlazorHub().RequireAuthorization(
    new AuthorizeAttribute
    {
        AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme
    });

app.Run();
