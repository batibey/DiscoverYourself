using System.Globalization;
using DiscoverYourself.Data;
using Microsoft.EntityFrameworkCore;
using DiscoverYourself.Managers;
using Microsoft.AspNetCore.Localization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day, 
        retainedFileCountLimit: 7, // max 7 day
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

// Add Serilog to the builder
builder.Host.UseSerilog();

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Configure session
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Mandatory for GDPR compliance
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
});

builder.Services.AddDbContext<DiscoverYourselfDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configure culture settings
var supportedCultures = new[] { new CultureInfo("tr"), new CultureInfo("en") };
var defaultCulture = new CultureInfo("en");

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

// Add CookieRequestCultureProvider to store selected culture
localizationOptions.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
localizationOptions.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider()); // Add Cookie support

var app = builder.Build();

// Auto migrate
app.MigrateDatabase<DiscoverYourselfDbContext>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Enable session middleware
app.UseRequestLocalization(localizationOptions); // Enable localization middleware
app.UseAuthorization();

// Log application start
Log.Information("Application started.");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start.");
}
finally
{
    Log.CloseAndFlush(); // Ensure all logs are written before application exits
}
