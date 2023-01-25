using System;
using authapi.data.Concrete;
using authapi.entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace authapi.Models
{
    public class AuthContextIdentity:IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AuthDb");
        }
        public DbSet<AppUser>Users{get;set;} //buna baxarsan
    }
}
