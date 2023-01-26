using authapi.data.Concrete;
using authapi.entity;
using authapi.Models;
using Microsoft.AspNetCore.Identity;
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

app.MapGet("/", () => "Hello World!");
                              
app.Run();
