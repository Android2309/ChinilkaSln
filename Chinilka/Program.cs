using BlazorBootstrap;
using Chinilka.Interfaces;
using Chinilka.Models;
using Chinilka.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ChinilkaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ChinilkaConnection"]);

    // Зачем такое в проде?
    options.EnableSensitiveDataLogging(true);
});

builder.Services.AddScoped<IChinilkaRepository, EFChinilkaRepository>();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"]);

});
builder.Services.AddIdentity<ChinilkaUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole",
         policy => policy.RequireRole("Admin"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "RequireAdminRole");
});
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddBlazorBootstrap();


builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
    opts.User.RequireUniqueEmail = true;
});

var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/error");
}

app.UseRequestLocalization(opts =>
{
    opts.AddSupportedCultures("en-US", "ru-ru")
    .AddSupportedUICultures("en-US", "ru-ru")
    .SetDefaultCulture("ru-ru");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("productspage", "catalog/{devicemodel}/page{productPage:int}",
    new { Controller = "Home", action = "Products", productPage = 1 });
app.MapControllerRoute("searchedproductspage", "products/search-{searchString}/page{productPage:int}",
    new { Controller = "Home", action = "SearchedProducts", productPage = 1 });
app.MapControllerRoute("category", "catalog/{category}",
    new { Controller = "Home", action = "DeviceModels" });

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();
