using authapi.data.Concrete;
using authapi.entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

IConfiguration _configuration;
_configuration=builder.Configuration;

builder.Services.AddDbContext<AuthContext>(options => options.UseSqlite(_configuration.GetConnectionString("Sqlite")));
builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AuthContext>();
 builder.Services.AddMvc();
var app = builder.Build();

app.UseAuthentication();
app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");
                              
app.Run();
