using System.Globalization;
using DiscoverYourself.Data;
using Microsoft.EntityFrameworkCore;
using DiscoverYourself.Managers;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization() // Add view localization support
    .AddDataAnnotationsLocalization(); // Enable localization for validation messages

// Configure session
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Mandatory for GDPR compliance
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
});

// Configure Entity Framework and database
builder.Services.AddDbContext<DiscoverYourselfDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Configure culture settings
var supportedCultures = new[] { new CultureInfo("tr"), new CultureInfo("en") };
var defaultCulture = new CultureInfo("en"); // Set default culture to English

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>() // Force default culture
};

// Add QueryStringRequestCultureProvider for culture switching
localizationOptions.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());

var app = builder.Build();

// Auto migrate
app.MigrateDatabase<DiscoverYourselfDbContext>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enable HSTS for production
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve static files

app.UseRouting();

app.UseSession(); // Enable session middleware
app.UseRequestLocalization(localizationOptions); // Enable localization middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
