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
using BMS.BlazorWebApp.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
GlobalAppSettings.BMSMSSql = builder.Configuration.GetConnectionString(Constants.connectionStringName);
GlobalAppSettings.MigrationsAssemblyName = Assembly.GetExecutingAssembly().FullName;

builder.Services.AddHttpClient();
builder.Services.AddScoped<TokenProvider>();

builder.Services.AddDbContext<BMSDbContext>(options => options.UseSqlServer(GlobalAppSettings.BMSMSSql, b => b.MigrationsAssembly(GlobalAppSettings.MigrationsAssemblyName)));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<BMSDbContext>();

builder.Services.AddBMSRepositories();
builder.Services.AddBMSServices();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<OpenIdConnectOptions>(
    OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.SaveTokens = true;

        options.Scope.Add(Constants.offlineAccess);
    });

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(Constants.errorPage);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage(Constants.hostPage);

app.MapBlazorHub().RequireAuthorization(
    new AuthorizeAttribute
    {
        AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme
    });

app.Run();
