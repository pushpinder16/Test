using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Test3.Data;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();
builder.Services.AddLogging();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.SlidingExpiration = true; // Enable sliding expiration
        options.Cookie.IsEssential = true; // Ensure the cookie is essential for authentication
        options.Cookie.SameSite = SameSiteMode.None; // Adjust SameSite settings as needed
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure the cookie is sent over HTTPS
    });


builder.Services.AddSession(Option =>
{
    Option.IdleTimeout = TimeSpan.FromMinutes(60);
    Option.Cookie.HttpOnly = true;
    Option.Cookie.IsEssential = true;

}

 );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
