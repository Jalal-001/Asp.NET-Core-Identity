using authapi.data.Concrete;
using authapi.entity;
using authapi.Models.Context;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

IConfiguration _configuration;
_configuration=builder.Configuration;

builder.Services.AddDbContext<AuthContextIdentity>(options => options.UseSqlite(_configuration.GetConnectionString("Sqlite")));
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<AuthContextIdentity>();
 builder.Services.AddMvc();
var app = builder.Build();

app.UseAuthentication();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
