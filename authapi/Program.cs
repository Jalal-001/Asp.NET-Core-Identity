using authapi.CustomValidations;
using authapi.data.Concrete;
using authapi.entity;
using authapi.Models.Context;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

IConfiguration _configuration;
_configuration=builder.Configuration;

builder.Services.AddDbContext<AuthContextIdentity>(options => options.UseSqlite(_configuration.GetConnectionString("Sqlite")));
builder.Services.AddIdentity<AppUser,AppRole>(o=>{
    o.Password.RequiredLength=5;
    o.Password.RequireDigit=false;
    o.Password.RequireUppercase=false;
    o.Password.RequireLowercase=false;
    o.Password.RequireNonAlphanumeric=false;
}).AddPasswordValidator<CustomPasswordValidation>()
.AddUserValidator<CustomUserValidation>()
.AddEntityFrameworkStores<AuthContextIdentity>();

builder.Services.AddMvc();







var app = builder.Build();

app.UseAuthentication();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}"

    );

app.Run();
