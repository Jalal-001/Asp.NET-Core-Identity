using System.Security;
using System.Net;
using authapi.CustomValidations;
using authapi.entity;
using authapi.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

IConfiguration _configuration;
_configuration = builder.Configuration;

builder.Services.AddDbContext<AuthContextIdentity>(options => options.UseSqlite(_configuration.GetConnectionString("Sqlite")));

builder.Services.AddIdentity<AppUser, AppRole>(o =>
{
    o.Password.RequiredLength = 5;
    o.Password.RequireDigit = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireNonAlphanumeric = false;
}).AddPasswordValidator<CustomPasswordValidation>() //Custom Password Validation
.AddUserValidator<CustomUserValidation>() //Custom User Validation
.AddErrorDescriber<CustomIdentityErrorDescriber>() //Custom Error describer
.AddEntityFrameworkStores<AuthContextIdentity>()
.AddDefaultTokenProviders(); //For using forgot password 

builder.Services.AddMvc();

// Cookie configuration
builder.Services.ConfigureApplicationCookie(c =>
{
    c.LoginPath = new PathString("/User/Login");
    c.Cookie = new CookieBuilder
    {
        Name = "AuthApiCookie",
        HttpOnly = false,
        // Expiration = TimeSpan.FromMinutes(3),
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.Always
    };

    c.SlidingExpiration = true;
    c.Cookie.MaxAge = TimeSpan.FromMinutes(3);
    // c.ExpireTimeSpan = TimeSpan.FromMinutes(3);
});



var app = builder.Build();

app.UseAuthentication();


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization(); //must be beetwen routing and mapControllerRoute


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}"

    );

app.Run();