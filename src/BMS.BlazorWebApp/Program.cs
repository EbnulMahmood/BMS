using BMS.Plugins.EFCore.Data;
using BMS.Plugins.EFCore.Extensions;
using BMS.Plugins.Dapper.Extensions;
using BMS.UseCases.Extensions;
using Microsoft.EntityFrameworkCore;
using BMS.BlazorWebApp.Securities;
using BMS.BlazorWebApp.Settings;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using BMS.CoreBusiness.Entities.Membership;
using Serilog;

var configuration = new ConfigurationBuilder()
           .AddJsonFile(path: Constants.appsettingsFileName)
           .Build();

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    GlobalAppSettings.BMSMSSql = builder.Configuration.GetConnectionString(Constants.connectionStringName);
    GlobalAppSettings.MigrationsAssemblyName = Assembly.GetExecutingAssembly().FullName;

    builder.Services.AddHttpClient();
    builder.Services.AddScoped<TokenProvider>();

    builder.Services.AddDbContextFactory<BMSDbContext>(options => options.UseSqlServer(GlobalAppSettings.BMSMSSql, b => b.MigrationsAssembly(GlobalAppSettings.MigrationsAssemblyName)));

    builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<BMSDbContext>();

    builder.Services.AddBMSRepositories();
    builder.Services.AddDapperRepositories();
    builder.Services.AddBMSServices();

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    builder.Services.AddAuthorizationSettings();

    builder.Host.UseSerilog();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

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

    app.MapRazorPages();

    app.MapBlazorHub();
    app.MapFallbackToPage(Constants.hostPage);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
